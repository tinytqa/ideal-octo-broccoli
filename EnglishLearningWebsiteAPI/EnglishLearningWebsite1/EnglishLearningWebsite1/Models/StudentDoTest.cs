using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("studentDoTest")]
public partial class StudentDoTest
{
    [Key]
    [Column("doId")]
    [StringLength(255)]
    public string DoId { get; set; } = null!;

    [Column("testId")]
    [StringLength(255)]
    public string? TestId { get; set; }

    [Column("studentId")]
    [StringLength(255)]
    public string? StudentId { get; set; }

    [Column("testDate", TypeName = "datetime")]
    public DateTime? TestDate { get; set; }

    [Column("result")]
    [StringLength(255)]
    public string? Result { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("StudentDoTests")]
    public virtual Student? Student { get; set; }

    [ForeignKey("TestId")]
    [InverseProperty("StudentDoTests")]
    public virtual Test? Test { get; set; }
}
