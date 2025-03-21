using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

[Table("User")]
public partial class User
{
    [Key]
    [Column("userId")]
    [StringLength(255)]
    public string UserId { get; set; } = null!;

    [Column("username")]
    [StringLength(255)]
    public string? Username { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string? Password { get; set; }

    [Column("fullName")]
    [StringLength(255)]
    public string? FullName { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("phoneNumber")]
    [StringLength(255)]
    public string? PhoneNumber { get; set; }

    [Column("role")]
    [StringLength(255)]
    public string? Role { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    [InverseProperty("User")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
