using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("Chapter")]
public partial class Chapter
{
    [Key]
    [Column("chapterId")]
    [StringLength(255)]
    public string ChapterId { get; set; } = null!;

    [Column("courseId")]
    [StringLength(255)]
    public string? CourseId { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Chapters")]
    public virtual Course? Course { get; set; }

    [InverseProperty("Chapter")]
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
