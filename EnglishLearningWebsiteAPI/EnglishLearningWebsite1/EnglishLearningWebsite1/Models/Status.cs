using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("Status")]
public partial class Status
{
    [Key]
    [Column("statusId")]
    [StringLength(255)]
    public string? StatusId { get; set; }

    [Column("studentId")]
    [StringLength(255)]
    public string? StudentId { get; set; }

    [Column("courseId")]
    [StringLength(255)]
    public string? CourseId { get; set; }

    [Column("progress")]
    [StringLength(255)]
    public string? Progress { get; set; }

    [Column("status")]
    [StringLength(255)]
    public string? Status1 { get; set; }

    [Column("completionDate", TypeName = "datetime")]
    public DateTime? CompletionDate { get; set; }

    [ForeignKey("CourseId")]
    public virtual Course? Course { get; set; }

    [ForeignKey("StudentId")]
    public virtual Student? Student { get; set; }
}
