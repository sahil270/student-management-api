using AutoMapper;
using Core;
using Data.Models;
using Dto;
using Microsoft.EntityFrameworkCore;
using Repo;

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

        public async Task<List<ClassDto>?> GetAllClassesAsync()
        {
            return await _classRepository.GetAll().Select(x => _mapper.Map<ClassDto>(x)).ToListAsync();
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
