using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("Video")]
public partial class Video
{
    [Key]
    [Column("videoId")]
    [StringLength(255)]
    public string VideoId { get; set; } = null!;

    [Column("lessonId")]
    [StringLength(255)]
    public string? LessonId { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("fileUrl")]
    [StringLength(255)]
    public string? FileUrl { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [ForeignKey("LessonId")]
    [InverseProperty("Videos")]
    public virtual Lesson? Lesson { get; set; }
}
