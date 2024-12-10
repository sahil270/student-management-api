using AutoMapper;
using Core;
using Data.Models;
using Dto;
using Microsoft.EntityFrameworkCore;
using Repo;
using System.Linq.Expressions;

namespace Service
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Class> _classRepository;
        private readonly IMapper _mapper;
        private bool disposedValue;

        public StudentService(IRepository<Student> studentRepository, IMapper mapper, IRepository<Class> classRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _classRepository = classRepository;
        }

        public async Task<PagedResultDto?> GetAllStudentesAsync(PageFilterParams model)
        {

            var searchPattern = model.SearchKeyword!.ToFilterPattern();

            Expression<Func<Student, bool>> filter = x => EF.Functions.Like(x.FirstName, searchPattern)
                                                                || EF.Functions.Like(x.LastName, searchPattern)
                                                                || EF.Functions.Like(x.EmailId, searchPattern)
                                                                || EF.Functions.Like(x.PhoneNumber, searchPattern)
                                                                || ( x.Class != null
                                                                && EF.Functions.Like(x.Class.Name, searchPattern));

            var resultQuery = _studentRepository.GetAll(filter).Include(x => x.Class)
                            .Select(x => _mapper.Map<StudentDto>(x));

            if (!string.IsNullOrEmpty(model.SortBy))
            {
                Expression<Func<Student, object>> sort = model.SortBy.BuildSortExpression<Student>();

                if (model.IsDescending)
                    resultQuery = resultQuery.OrderByDescending(sort);
                else
                    resultQuery = resultQuery.OrderBy(sort);
            }

            var totalCount = await resultQuery.CountAsync();
            var data = await resultQuery.Skip((model.Page - 1) * model.PageSize).Take(model.PageSize).ToListAsync();

            return new PagedResultDto()
            {
                TotalCount = totalCount,
                Data = data,
            };
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            var StudentObj = await _studentRepository.GetFirst(x => x.StudentId.Equals(id));

            if (StudentObj == null)
                throw new APIException(400, $"No Student found with Id: {id}");


            return _mapper.Map<StudentDto>(StudentObj);
        }

        public async Task<List<StudentDto>?> GetAllStudentByClassIdAsync(int classId)
        {
            var studentsList = await _studentRepository.GetAll(x => x.ClassId.Equals(classId))
                .Select(x => _mapper.Map<StudentDto>(x))
                .ToListAsync();

            if (studentsList == null)
                throw new APIException(400, $"No Student found with Class Id: {classId}");


            return studentsList;
        }

        public async Task<StudentDto?> AddStudentAsync(StudentDto dto)
        {
            var StudentObj = _mapper.Map<Student>(dto);

            StudentObj = await _studentRepository.Add(StudentObj!);

            return _mapper.Map<StudentDto>(StudentObj);
        }

        public async Task<StudentDto?> UpdateStudentAsync(int id, StudentDto dto)
        {
            var studentObj = await _studentRepository.GetFirst(x => x.StudentId.Equals(id), true);

            if (studentObj == null)
                throw new APIException(400, $"No Student found with Id: {id}");

            studentObj.ClassId = dto.ClassId;
            studentObj.PhoneNumber = dto.PhoneNumber;
            studentObj.LastName = dto.LastName;
            studentObj.FirstName = dto.FirstName;
            studentObj.EmailId = dto.EmailId;

            studentObj = await _studentRepository.Update(studentObj!);

            return _mapper.Map<StudentDto>(studentObj);
        }

        public async Task<StudentDto?> AssignStudentToClass(int id, int classId)
        {
            var studentObj = await _studentRepository.GetFirst(x => x.StudentId.Equals(id), true);
            var classObj = await _classRepository.GetFirst(x => x.ClassId.Equals(id), true);

            if (studentObj == null)
                throw new APIException(400, $"No Student found with Id: {id}");

            if (classObj == null)
                throw new APIException(400, $"No Class found with Id: {classId}");

            studentObj.ClassId = classId;

            studentObj = await _studentRepository.Update(studentObj!);

            return _mapper.Map<StudentDto>(studentObj);
        }

        public async Task<StudentDto?> DeleteStudentAsync(int id)
        {
            var StudentObj = await _studentRepository.GetFirst(x => x.StudentId.Equals(id), true);

            if (StudentObj == null)
                throw new APIException(400, $"No Student found with Id: {id}");

            StudentObj = await _studentRepository.Delete(StudentObj!);

            return _mapper.Map<StudentDto>(StudentObj);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _studentRepository?.Dispose();
                }

                GC.SuppressFinalize(this);
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }
    }
}
