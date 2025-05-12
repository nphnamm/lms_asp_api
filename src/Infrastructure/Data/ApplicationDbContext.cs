using System;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure Identity tables
        builder.Entity<User>(e =>
        {
            e.ToTable("Users");
        });
        builder.Entity<IdentityUserClaim<Guid>>(e =>
        {
            e.ToTable("UserClaims");
        });
        builder.Entity<IdentityUserLogin<Guid>>(e =>
        {
            e.ToTable("UserLogins");
        });
        builder.Entity<IdentityUserToken<Guid>>(e =>
        {
            e.ToTable("UserTokens");
        });
        builder.Entity<IdentityRole<Guid>>(e =>
        {
            e.ToTable("Roles");
        });
        builder.Entity<IdentityRoleClaim<Guid>>(e =>
        {
            e.ToTable("RoleClaims");
        });
        builder.Entity<IdentityUserRole<Guid>>(e =>
        {
            e.ToTable("UserRoles");
        });

        // Configure Course
        builder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Instructor)
                .WithMany()
                .HasForeignKey(e => e.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Lesson
        builder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Course)
                .WithMany(c => c.Lessons)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Enrollment
        builder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Question
        builder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Lesson)
                .WithMany(l => l.Questions)
                .HasForeignKey(e => e.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Option
        builder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Question)
                .WithMany(q => q.Options)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}