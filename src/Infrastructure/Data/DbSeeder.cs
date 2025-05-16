using System;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.Enums;

namespace Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        // Remove old data
        context.Options.RemoveRange(context.Options);
        context.Questions.RemoveRange(context.Questions);
        context.Lessons.RemoveRange(context.Lessons);
        context.Enrollments.RemoveRange(context.Enrollments);
        context.Courses.RemoveRange(context.Courses);
        context.Media.RemoveRange(context.Media);
        context.Users.RemoveRange(context.Users);
        context.Roles.RemoveRange(context.Roles);
        await context.SaveChangesAsync();

        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Seed roles
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
        }
        if (!await roleManager.RoleExistsAsync("Instructor"))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>("Instructor"));
        }
        if (!await roleManager.RoleExistsAsync("Student"))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>("Student"));
        }

        // Seed admin user
        var adminEmail = "admin@lms.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };
            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Seed instructor
        var instructorEmail = "instructor@lms.com";
        var instructor = await userManager.FindByEmailAsync(instructorEmail);
        if (instructor == null)
        {
            instructor = new User
            {
                UserName = instructorEmail,
                Email = instructorEmail,
                FirstName = "John",
                LastName = "Doe",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };
            var result = await userManager.CreateAsync(instructor, "Instructor123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(instructor, "Instructor");
            }
        }

        // Seed student
        var studentEmail = "student@lms.com";
        var student = await userManager.FindByEmailAsync(studentEmail);
        if (student == null)
        {
            student = new User
            {
                UserName = studentEmail,
                Email = studentEmail,
                FirstName = "Jane",
                LastName = "Smith",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };
            var result = await userManager.CreateAsync(student, "Student123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(student, "Student");
            }
        }

        // Seed sample course
        if (!await context.Courses.AnyAsync())
        {
            var course = new Course
            {
                Id = Guid.NewGuid(),
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming with this comprehensive course.",
                Price = 49.99m,
                InstructorId = instructor.Id,
                ImageUrl = "https://via.placeholder.com/150",
                CreatedAt = DateTime.UtcNow,
                IsPublished = true
            };

            context.Courses.Add(course);
            await context.SaveChangesAsync();

            // Add sample media
            var media = new Media
            {
                Id = Guid.NewGuid(),
                FileName = "course-image.jpg",
                ContentType = "Image",
                Url = "https://via.placeholder.com/150",
                EntityId = course.Id,
                EntityType = "Course",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            context.Media.Add(media);
            await context.SaveChangesAsync();


            // Add sample lessons
            var lesson1 = new Lesson
            {
                Id = Guid.NewGuid(),
                Title = "Getting Started",
                Content = "Welcome to the course! In this lesson, we'll cover the basics.",
                Order = 1,
                CourseId = course.Id,
                CreatedAt = DateTime.UtcNow,
                IsPublished = true,
                Type = LessonType.Text,
            };

            var lesson2 = new Lesson
            {
                Id = Guid.NewGuid(),
                Title = "Variables and Data Types",
                Content = "Learn about different types of variables and how to use them.",
                Order = 2,
                CourseId = course.Id,
                CreatedAt = DateTime.UtcNow,
                IsPublished = true,
                Type = LessonType.MultipleChoice
            };

            context.Lessons.AddRange(lesson1, lesson2);
            await context.SaveChangesAsync();

            // Add questions and options for the MultipleChoice lesson
            if (lesson2.Type == LessonType.MultipleChoice)
            {
                var question1 = new Question
                {
                    Id = Guid.NewGuid(),
                    LessonId = lesson2.Id,
                    Text = "What is 2 + 2?",
                    CreatedAt = DateTime.UtcNow
                };

                var options = new[]
                {
                    new Option
                    {
                        Id = Guid.NewGuid(),
                        Text = "3",
                        IsCorrect = false,
                        Order = 1,
                        QuestionId = question1.Id
                    },
                    new Option
                    {
                        Id = Guid.NewGuid(),
                        Text = "4",
                        IsCorrect = true,
                        Order = 2,
                        QuestionId = question1.Id
                    },
                    new Option
                    {
                        Id = Guid.NewGuid(),
                        Text = "5",
                        IsCorrect = false,
                        Order = 3,
                        QuestionId = question1.Id
                    }
                };

                context.Questions.Add(question1);
                context.Options.AddRange(options);
                await context.SaveChangesAsync();
            }

            // Add sample enrollment
            var enrollment = new Enrollment
            {
                Id = Guid.NewGuid(),
                CourseId = course.Id,
                StudentId = student.Id,
                EnrollmentDate = DateTime.UtcNow,
                Status = EnrollmentStatus.Active
            };

            context.Enrollments.Add(enrollment);
            await context.SaveChangesAsync();
        }
    }
}