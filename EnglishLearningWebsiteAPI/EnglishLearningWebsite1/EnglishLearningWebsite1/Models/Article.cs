using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("Article")]
public partial class Article
{
    [Key]
    [Column("articleId")]
    [StringLength(255)]
    public string ArticleId { get; set; } = null!;

    [Column("instructorId")]
    [StringLength(255)]
    public string? InstructorId { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("articalContent")]
    [StringLength(255)]
    public string? ArticalContent { get; set; }

    [Column("postedDate", TypeName = "datetime")]
    public DateTime? PostedDate { get; set; }

    [Column("courseId")]
    [StringLength(255)]
    public string? CourseId { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Articles")]
    public virtual Course? Course { get; set; }

    [ForeignKey("InstructorId")]
    [InverseProperty("Articles")]
    public virtual Instructor? Instructor { get; set; }
}
