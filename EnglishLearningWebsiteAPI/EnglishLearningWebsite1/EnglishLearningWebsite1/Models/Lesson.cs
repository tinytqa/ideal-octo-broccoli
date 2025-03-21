using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("Lesson")]
public partial class Lesson
{
    [Key]
    [StringLength(255)]
    public string LessonId { get; set; } = null!;

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("chapterId")]
    [StringLength(255)]
    public string? ChapterId { get; set; }

    [ForeignKey("ChapterId")]
    [InverseProperty("Lessons")]
    public virtual Chapter? Chapter { get; set; }

    [InverseProperty("Lesson")]
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    [InverseProperty("Lesson")]
    public virtual ICollection<Quizz> Quizzs { get; set; } = new List<Quizz>();

    [InverseProperty("Lesson")]
    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
