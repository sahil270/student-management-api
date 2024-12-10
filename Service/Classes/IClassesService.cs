using Dto;

namespace Service
{
    public interface IClassesService : IDisposable
    {
        Task<ClassDto?> AddClassAsync(ClassDto dto);
        Task<ClassDto?> DeleteClassAsync(int id, ClassDto dto);
        Task<ClassDto?> GetAllClassByIdAsync(int id);
        Task<List<ClassDto>?> GetAllClassesAsync();
        Task<ClassDto?> UpdateClassAsync(int id, ClassDto dto);
    }
}