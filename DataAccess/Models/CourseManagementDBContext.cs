using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Models
{
    public partial class CourseManagementDBContext : DbContext
    {
        public CourseManagementDBContext()
        {
        }

        public CourseManagementDBContext(DbContextOptions<CourseManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assessment> Assessments { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Mark> Marks { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
              if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DB"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Percent).HasColumnName("percent");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Assessments)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Assessmen__cours__4316F928");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => e.Code, "UQ__Courses__357D4CF9F60B176D")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Credits).HasColumnName("credits");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Departme__A25C5AA6D8AA5CCD");

                entity.HasIndex(e => e.Name, "UQ__Departme__737584F6435796AD")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("enrollment");

                entity.HasIndex(e => new { e.StudentId, e.CourseId, e.SemesterId }, "UQ__enrollme__C949A1BED1554A28")
                    .IsUnique();

                entity.Property(e => e.EnrollmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("enrollmentId");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.SemesterId).HasColumnName("semesterId");

                entity.Property(e => e.StudentId).HasColumnName("studentId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__enrollmen__cours__47DBAE45");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK__enrollmen__semes__48CFD27E");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__enrollmen__stude__46E78A0C");
            });

            modelBuilder.Entity<Mark>(entity =>
            {
                entity.HasKey(e => new { e.EnrollmentId, e.AssessmentId })
                    .HasName("PK__marks__90886EF1AD6890BD");

                entity.ToTable("marks");

                entity.Property(e => e.EnrollmentId).HasColumnName("enrollmentId");

                entity.Property(e => e.AssessmentId).HasColumnName("assessmentId");

                entity.Property(e => e.Mark1)
                    .HasColumnType("decimal(5, 1)")
                    .HasColumnName("mark");

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__marks__assessmen__4CA06362");

                entity.HasOne(d => d.Enrollment)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.EnrollmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__marks__enrollmen__4BAC3F29");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("semesters");

                entity.HasIndex(e => e.Code, "UQ__semester__357D4CF9D92E0526")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.BeginDate)
                    .HasColumnType("date")
                    .HasColumnName("beginDate");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("date")
                    .HasColumnName("birthdate");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Department)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("department");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.DepartmentNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.Department)
                    .HasConstraintName("FK__Students__depart__403A8C7D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
