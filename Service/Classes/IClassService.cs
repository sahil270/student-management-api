using Dto;

namespace Service
{
    public interface IClassService : IDisposable
    {
        Task<ClassDto?> AddClassAsync(ClassDto dto);
        Task<ClassDto?> DeleteClassAsync(int id);
        Task<ClassDto?> GetAllClassByIdAsync(int id);
        Task<List<ClassDto>?> GetAllClassesAsync();
        Task<ClassDto?> UpdateClassAsync(int id, ClassDto dto);
    }
}