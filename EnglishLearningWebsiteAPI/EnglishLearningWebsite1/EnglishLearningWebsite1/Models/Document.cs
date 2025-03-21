using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("Document")]
public partial class Document
{
    [Key]
    [Column("documentId")]
    [StringLength(255)]
    public string DocumentId { get; set; } = null!;

    [Column("lessonId")]
    [StringLength(255)]
    public string? LessonId { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("fileUrl")]
    [StringLength(255)]
    public string? FileUrl { get; set; }

    [ForeignKey("LessonId")]
    [InverseProperty("Documents")]
    public virtual Lesson? Lesson { get; set; }
}
