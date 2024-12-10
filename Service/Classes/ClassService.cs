using AutoMapper;
using Core;
using Data.Models;
using Dto;
using Microsoft.EntityFrameworkCore;
using Repo;
using System.Linq.Expressions;

namespace Service
{
    public class ClassService : IClassService
    {
        private readonly IRepository<Class> _classRepository;
        private readonly IMapper _mapper;
        private bool disposedValue;

        public ClassService(IRepository<Class> classRepository, IMapper mapper)
        {
            _classRepository = classRepository;
            _mapper = mapper;
        }

        public async Task<PagedResultDto> GetAllClassesAsync(PageFilterParams model)
        {
            var searchPattern = model.SearchKeyword!.ToFilterPattern();

            Expression<Func<Class, bool>> filter = x => EF.Functions.Like(x.Name, searchPattern);

            var resultQuery = _classRepository.GetAll(filter).Select(x => _mapper.Map<ClassDto>(x));

            if (!string.IsNullOrEmpty(model.SortBy))
            {
                Expression<Func<Class, object>> sort = model.SortBy.BuildSortExpression<Class>();

                if (model.IsDescending)
                    resultQuery = resultQuery.OrderByDescending(sort);
                else
                    resultQuery = resultQuery.OrderBy(sort);
            }

            var totalCount = await resultQuery.CountAsync();
            var data = await resultQuery.Skip((model.Page-1)* model.PageSize).Take(model.PageSize).ToListAsync();

            return new PagedResultDto()
            {
                TotalCount = totalCount,
                Data = data,
            };
        }

        public async Task<ClassDto?> GetAllClassByIdAsync(int id)
        {
            var classObj = await _classRepository.GetFirst(x => x.ClassId.Equals(id));

            if (classObj == null)
                throw new APIException(400, $"No Class found with Id: {id}");


            return _mapper.Map<ClassDto>(classObj);
        }

        public async Task<ClassDto?> AddClassAsync(ClassDto dto)
        {
            var classObj = _mapper.Map<Class>(dto);

            classObj = await _classRepository.Add(classObj!);

            return _mapper.Map<ClassDto>(classObj);
        }

        public async Task<ClassDto?> UpdateClassAsync(int id, ClassDto dto)
        {
            var classObj = await _classRepository.GetFirst(x => x.ClassId.Equals(id), true);

            if (classObj == null)
                throw new APIException(400, $"No Class found with Id: {id}");

            classObj.Description = dto.Description;
            classObj.Name = dto.Name;

            classObj = await _classRepository.Update(classObj!);

            return _mapper.Map<ClassDto>(classObj);
        }

        public async Task<ClassDto?> DeleteClassAsync(int id)
        {
            var classObj = await _classRepository.GetFirst(x => x.ClassId.Equals(id), true);

            if (classObj == null)
                throw new APIException(400, $"No Class found with Id: {id}");

            classObj = await _classRepository.Delete(classObj!);

            return _mapper.Map<ClassDto>(classObj);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _classRepository?.Dispose();
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
