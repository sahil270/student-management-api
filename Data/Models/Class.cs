using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public partial class Class
{
    [Key]
    public int ClassId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string? Description { get; set; }

    [InverseProperty("Class")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
