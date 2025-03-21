using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("Payment")]
public partial class Payment
{
    [Key]
    [Column("paymentId")]
    [StringLength(255)]
    public string PaymentId { get; set; } = null!;

    [Column("studentId")]
    [StringLength(255)]
    public string? StudentId { get; set; }

    [Column("courseId")]
    [StringLength(255)]
    public string? CourseId { get; set; }

    [Column("paymentDate", TypeName = "datetime")]
    public DateTime? PaymentDate { get; set; }

    [Column("amount", TypeName = "decimal(10, 2)")]
    public decimal? Amount { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Payments")]
    public virtual Course? Course { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Payments")]
    public virtual Student? Student { get; set; }
}
