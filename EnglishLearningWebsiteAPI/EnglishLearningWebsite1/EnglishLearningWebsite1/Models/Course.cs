using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("Course")]
public partial class Course
{
    [Key]
    [Column("courseId")]
    [StringLength(255)]
    public string CourseId { get; set; } = null!;

    [Column("instructorId")]
    [StringLength(255)]
    public string? InstructorId { get; set; }

    [Column("courseName")]
    [StringLength(255)]
    public string? CourseName { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [Column("createdAt", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("startDate", TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column("courseImage")]
    [StringLength(255)]
    public string? CourseImage { get; set; }

    [Column("shortTitle")]
    [StringLength(100)]
    public string? ShortTitle { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    [InverseProperty("Course")]
    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

    [ForeignKey("InstructorId")]
    [InverseProperty("Courses")]
    public virtual Instructor? Instructor { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [InverseProperty("Course")]
    public virtual ICollection<StudentBuyCourse> StudentBuyCourses { get; set; } = new List<StudentBuyCourse>();

    [InverseProperty("Course")]
    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
