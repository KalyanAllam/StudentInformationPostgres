using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SISPostgres.Models;

public partial class ContosoUniversityDataContext : DbContext
{
    public ContosoUniversityDataContext()
    {
    }

    public ContosoUniversityDataContext(DbContextOptions<ContosoUniversityDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Term> Terms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=ContosoUniversityData;Username=postgres;Password=superuser");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Classid).HasName("pk__class__cb1927a0a166e951");

            entity.ToTable("Class");

            entity.Property(e => e.Classid).HasColumnName("classid");
            entity.Property(e => e.Classname)
                .HasMaxLength(50)
                .HasColumnName("classname");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Courseid).HasName("pk__course__c92d71878c25379a");

            entity.ToTable("Course");

            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.Classid).HasColumnName("classid");
            entity.Property(e => e.Credits)
                .HasMaxLength(50)
                .HasColumnName("credits");
            entity.Property(e => e.Facultyid).HasColumnName("facultyid");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.Class).WithMany(p => p.Courses)
                .HasForeignKey(d => d.Classid)
                .HasConstraintName("fk_dbo.class_dbo.course_classid");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Courses)
                .HasForeignKey(d => d.Facultyid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_dbo.course_dbo.faculty_facultyid");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.Enrollmentid).HasName("pk__enrollme__7f6877fb43b17c50");

            entity.ToTable("Enrollment");

            entity.Property(e => e.Enrollmentid).HasColumnName("enrollmentid");
            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.Marks).HasColumnName("marks");
            entity.Property(e => e.Marksobtained).HasColumnName("marksobtained");
            entity.Property(e => e.Studentid).HasColumnName("studentid");
            entity.Property(e => e.Termid).HasColumnName("termid");
            entity.Property(e => e.Grade).HasColumnName("grade");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Courseid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_dbo.enrollment_dbo.course_courseid");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_dbo.enrollment_dbo.student_studentid");

            entity.HasOne(d => d.Term).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Termid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_dbo.term_dbo.student_termid");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Facultyid).HasName("pk__faculty__306f636e1530f78b");

            entity.ToTable("Faculty");

            entity.Property(e => e.Facultyid).HasColumnName("facultyid");
            entity.Property(e => e.Facultyfirstname)
                .HasMaxLength(50)
                .HasColumnName("facultyfirstname ");
            entity.Property(e => e.Facultylastname)
                .HasMaxLength(50)
                .HasColumnName("facultylastname");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Sectionid).HasName("pk__section__80ef0892521faf0f");

            entity.ToTable("Section");

            entity.Property(e => e.Sectionid).HasColumnName("sectionid");
            entity.Property(e => e.Classsectionname)
                .HasMaxLength(50)
                .HasColumnName(" classsectionname ");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Studentid).HasName("pk__student__32c52a7988904b48");

            entity.ToTable("Student");

            entity.Property(e => e.Studentid).HasColumnName("studentid");
            entity.Property(e => e.Classid).HasColumnName("classid");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName(" firstname ");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname ");
            entity.Property(e => e.Phoneno).HasColumnName(" phoneno ");
            entity.Property(e => e.Rollno).HasColumnName("rollno");
            entity.Property(e => e.Sectionid).HasColumnName(" sectionid ");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.Classid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_dbo.class_dbo.student_classid");

            entity.HasOne(d => d.Section).WithMany(p => p.Students)
                .HasForeignKey(d => d.Sectionid)
                .HasConstraintName("fk_dbo.section_dbo.student_sectionid");
        });

        modelBuilder.Entity<Term>(entity =>
        {
            entity.HasKey(e => e.Termid).HasName("pk__term__410a2e45e499cfeb");

            entity.ToTable("Term");

            entity.Property(e => e.Termid).HasColumnName("termid");
            entity.Property(e => e.Termname)
                .HasMaxLength(50)
                .HasColumnName("termname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
