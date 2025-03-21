using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("Instructor")]
public partial class Instructor
{
    [Key]
    [Column("instructorId")]
    [StringLength(255)]
    public string InstructorId { get; set; } = null!;

    [Column("userId")]
    [StringLength(255)]
    public string? UserId { get; set; }

    [Column("bio")]
    [StringLength(255)]
    public string? Bio { get; set; }

    [Column("expertise")]
    [StringLength(255)]
    public string? Expertise { get; set; }

    [InverseProperty("Instructor")]
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    [InverseProperty("Instructor")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    [ForeignKey("UserId")]
    [InverseProperty("Instructors")]
    public virtual User? User { get; set; }
}
