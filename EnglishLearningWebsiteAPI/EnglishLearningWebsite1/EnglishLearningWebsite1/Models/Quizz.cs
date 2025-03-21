using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("Quizz")]
public partial class Quizz
{
    [Key]
    [Column("quizzId")]
    [StringLength(255)]
    public string QuizzId { get; set; } = null!;

    [Column("lessonId")]
    [StringLength(255)]
    public string? LessonId { get; set; }

    [Column("questionUrl")]
    [StringLength(255)]
    public string? QuestionUrl { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("keyUrl")]
    [StringLength(255)]
    public string? KeyUrl { get; set; }

    [ForeignKey("LessonId")]
    [InverseProperty("Quizzs")]
    public virtual Lesson? Lesson { get; set; }
}
