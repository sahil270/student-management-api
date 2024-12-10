using Dto;

namespace Service
{
    public interface IStudentService: IDisposable
    {
        Task<StudentDto?> AddStudentAsync(StudentDto dto);
        Task<StudentDto?> AssignStudentToClass(int id, int classId);
        Task<StudentDto?> DeleteStudentAsync(int id);
        Task<List<StudentDto>?> GetAllStudentByClassIdAsync(int classId);
        Task<StudentDto?> GetStudentByIdAsync(int id);
        Task<PagedResultDto?> GetAllStudentesAsync(PageFilterParams model);
        Task<StudentDto?> UpdateStudentAsync(int id, StudentDto dto);
    }
}