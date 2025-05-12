// Infrastructure/Data/ApplicationDbContext.cs
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>(e =>
        {
            e.ToTable("Users");
        });
            //2
            builder.Entity<IdentityUserClaim<Guid>>(e =>
            {
                e.ToTable("UserClaims");
            });
            //3
            builder.Entity<IdentityUserLogin<Guid>>(e =>
            {
                e.ToTable("UserLogins");
            });
            //4
            builder.Entity<IdentityUserToken<Guid>>(e =>
            {
                e.ToTable("UserTokens");
            });
            //5
            builder.Entity<IdentityRole<Guid>>(e =>
            {
                e.ToTable("Roles");
            });
            //6
            builder.Entity<IdentityRoleClaim<Guid>>(e =>
            {
                e.ToTable("RoleClaims");
            });
            //7
            builder.Entity<IdentityUserRole<Guid>>(e =>
            {
                e.ToTable("UserRoles");
            });
        builder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Instructor)
                .WithMany()
                .HasForeignKey(e => e.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Course)
                .WithMany(c => c.Lessons)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

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
    }
}