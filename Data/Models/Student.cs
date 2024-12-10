using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Index("EmailId", Name = "UQ__Students__7ED91ACE9753B623", IsUnique = true)]
[Index("PhoneNumber", Name = "UQ__Students__85FB4E383CE3FCA9", IsUnique = true)]
public partial class Student
{
    [Key]
    public int StudentId { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(100)]
    public string EmailId { get; set; } = null!;

    public int ClassId { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("Students")]
    public virtual Class Class { get; set; } = null!;
}
