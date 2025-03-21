using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("Test")]
public partial class Test
{
    [Key]
    [Column("testId")]
    [StringLength(255)]
    public string TestId { get; set; } = null!;

    [Column("courseId")]
    [StringLength(255)]
    public string? CourseId { get; set; }

    [Column("field")]
    [StringLength(255)]
    public string? Field { get; set; }

    [Column("testContent")]
    [StringLength(255)]
    public string? TestContent { get; set; }

    [Column("type")]
    [StringLength(255)]
    public string? Type { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Tests")]
    public virtual Course? Course { get; set; }

    [InverseProperty("Test")]
    public virtual ICollection<StudentDoTest> StudentDoTests { get; set; } = new List<StudentDoTest>();
}
