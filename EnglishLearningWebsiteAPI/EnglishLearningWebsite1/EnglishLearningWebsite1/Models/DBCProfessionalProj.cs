using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite1.Models;

public partial class DBCProfessionalProj : DbContext
{
    public DBCProfessionalProj()
    {
    }

    public DBCProfessionalProj(DbContextOptions<DBCProfessionalProj> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Quizz> Quizzs { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentBuyCourse> StudentBuyCourses { get; set; }

    public virtual DbSet<StudentDoTest> StudentDoTests { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=cmcsv.ric.vn,10000;Initial Catalog=duc_dacn;Persist Security Info=True;User ID=cmcsv;Password=cM!@#2025;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__Article__75D3D37EF9F70C22");

            entity.HasOne(d => d.Course).WithMany(p => p.Articles).HasConstraintName("FK__Article__courseI__3BFFE745");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Articles).HasConstraintName("FK__Article__instruc__3B0BC30C");
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PK__Chapter__05D716CFD18FBA09");

            entity.HasOne(d => d.Course).WithMany(p => p.Chapters).HasConstraintName("FK__Chapter__courseI__10216507");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__2AA84FD19B32F159");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Courses).HasConstraintName("FK__Course__instruct__0D44F85C");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__EFAAAD853CF2462F");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Documents).HasConstraintName("FK__Document__lesson__18B6AB08");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InstructorId).HasName("PK__Instruct__0031FA44667AEDFA");

            entity.HasOne(d => d.User).WithMany(p => p.Instructors).HasConstraintName("FK__Instructo__userI__078C1F06");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lesson__B084ACD076C5FB6D");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Lessons).HasConstraintName("FK__Lesson__chapterI__12FDD1B2");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__A0D9EFC6AABE9F56");

            entity.HasOne(d => d.Course).WithMany(p => p.Payments).HasConstraintName("FK__Payment__courseI__2334397B");

            entity.HasOne(d => d.Student).WithMany(p => p.Payments).HasConstraintName("FK__Payment__student__22401542");
        });

        modelBuilder.Entity<Quizz>(entity =>
        {
            entity.HasKey(e => e.QuizzId).HasName("PK__Quizz__3EBD2BF4F30F06C3");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Quizzs).HasConstraintName("FK__Quizz__lessonId__1B9317B3");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasOne(d => d.Course).WithMany().HasConstraintName("FK__Status__courseId__4B422AD5");

            entity.HasOne(d => d.Student).WithMany().HasConstraintName("FK__Status__studentI__4A4E069C");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__4D11D63C7B644AF8");

            entity.HasOne(d => d.User).WithMany(p => p.Students).HasConstraintName("FK__Student__userId__0A688BB1");
        });

        modelBuilder.Entity<StudentBuyCourse>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__studentB__ACFF2C866ECFD028");

            entity.HasOne(d => d.Course).WithMany(p => p.StudentBuyCourses).HasConstraintName("FK__studentBu__cours__2DB1C7EE");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentBuyCourses).HasConstraintName("FK__studentBu__stude__2CBDA3B5");
        });

        modelBuilder.Entity<StudentDoTest>(entity =>
        {
            entity.HasKey(e => e.DoId).HasName("PK__studentD__2C94C5EA8D19E11A");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentDoTests).HasConstraintName("FK__studentDo__stude__42ACE4D4");

            entity.HasOne(d => d.Test).WithMany(p => p.StudentDoTests).HasConstraintName("FK__studentDo__testI__41B8C09B");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__A29BFB88F902F0A2");

            entity.HasOne(d => d.Course).WithMany(p => p.Tests).HasConstraintName("FK__Test__courseId__3EDC53F0");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFFF45B506D");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.VideoId).HasName("PK__Video__14B0F5B67358E45B");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Videos).HasConstraintName("FK__Video__lessonId__15DA3E5D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
