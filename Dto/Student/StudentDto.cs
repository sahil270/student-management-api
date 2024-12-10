using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class StudentDto
    {
        public int StudentId { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [MaxLength(10)]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(100)]
        public string EmailId { get; set; } = null!;

        public int ClassId { get; set; }

        public virtual ClassDto Class { get; set; } = null!;
    }
}
