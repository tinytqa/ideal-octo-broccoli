using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("studentBuyCourse")]
public partial class StudentBuyCourse
{
    [Key]
    [Column("enrollmentId")]
    [StringLength(255)]
    public string EnrollmentId { get; set; } = null!;

    [Column("studentId")]
    [StringLength(255)]
    public string? StudentId { get; set; }

    [Column("courseId")]
    [StringLength(255)]
    public string? CourseId { get; set; }

    [Column("enrollmentDate", TypeName = "datetime")]
    public DateTime? EnrollmentDate { get; set; }

    [Column("status")]
    [StringLength(255)]
    public string? Status { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("StudentBuyCourses")]
    public virtual Course? Course { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("StudentBuyCourses")]
    public virtual Student? Student { get; set; }
}
