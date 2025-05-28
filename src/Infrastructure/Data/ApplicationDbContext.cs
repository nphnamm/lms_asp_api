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
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<CourseProgress> CourseProgress { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Media> Media { get; set; }
    public DbSet<ExerciseHistory> ExerciseHistories { get; set; }
    public DbSet<QuestionHistory> QuestionHistories { get; set; }
    
    // New analytics DbSets
    public DbSet<VisitorAnalytics> VisitorAnalytics { get; set; }
    public DbSet<CourseAnalytics> CourseAnalytics { get; set; }
    public DbSet<LessonAnalytics> LessonAnalytics { get; set; }

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

        // Configure Media
        builder.Entity<Media>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.EntityType, e.EntityId });
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

        // Configure CourseProgress
        builder.Entity<CourseProgress>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Course)
                .WithMany(c => c.CourseProgress)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Question
        builder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Exercise)
                .WithMany(l => l.Questions)
                .HasForeignKey(e => e.ExerciseId)
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

        // Configure ExerciseHistory
        builder.Entity<ExerciseHistory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Exercise)
                .WithMany(l => l.ExerciseHistories)
                .HasForeignKey(e => e.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure QuestionHistory
        builder.Entity<QuestionHistory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Question)
                .WithMany(q => q.QuestionHistories)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure VisitorAnalytics
        builder.Entity<VisitorAnalytics>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.VisitDate);
            entity.HasIndex(e => new { e.UserId, e.CourseId, e.LessonId });
        });

        // Configure CourseAnalytics
        builder.Entity<CourseAnalytics>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.CourseId, e.Date });
            entity.HasOne(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure LessonAnalytics
        builder.Entity<LessonAnalytics>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.LessonId, e.Date });
            entity.HasOne(e => e.Lesson)
                .WithMany()
                .HasForeignKey(e => e.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}