using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class ClassDto
    {
        public int ClassId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string? Description { get; set; }

        public virtual ICollection<StudentDto>? Students { get; set; } = [];
    }
}
