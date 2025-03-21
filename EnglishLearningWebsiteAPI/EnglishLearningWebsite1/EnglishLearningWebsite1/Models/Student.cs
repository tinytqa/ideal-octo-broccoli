using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("Student")]
public partial class Student
{
    [Key]
    [Column("studentId")]
    [StringLength(255)]
    public string StudentId { get; set; } = null!;

    [Column("userId")]
    [StringLength(255)]
    public string? UserId { get; set; }

    [Column("dob", TypeName = "datetime")]
    public DateTime? Dob { get; set; }

    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentBuyCourse> StudentBuyCourses { get; set; } = new List<StudentBuyCourse>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentDoTest> StudentDoTests { get; set; } = new List<StudentDoTest>();

    [ForeignKey("UserId")]
    [InverseProperty("Students")]
    public virtual User? User { get; set; }
}
