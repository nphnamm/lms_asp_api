using System;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.Enums;
using System.Collections.Generic;

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
        context.CourseProgress.RemoveRange(context.CourseProgress);
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

        // Seed instructors
        var instructors = new[]
        {
            new { Email = "john.doe@lms.com", FirstName = "John", LastName = "Doe", Password = "Instructor123!" },
            new { Email = "sarah.smith@lms.com", FirstName = "Sarah", LastName = "Smith", Password = "Instructor123!" }
        };

        foreach (var instructorData in instructors)
        {
            var instructor = await userManager.FindByEmailAsync(instructorData.Email);
            if (instructor == null)
            {
                instructor = new User
                {
                    UserName = instructorData.Email,
                    Email = instructorData.Email,
                    FirstName = instructorData.FirstName,
                    LastName = instructorData.LastName,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };
                var result = await userManager.CreateAsync(instructor, instructorData.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(instructor, "Instructor");
                }
            }
        }

        // Seed students
        var students = new[]
        {
            new { Email = "jane.smith@lms.com", FirstName = "Jane", LastName = "Smith", Password = "Student123!" },
            new { Email = "mike.johnson@lms.com", FirstName = "Mike", LastName = "Johnson", Password = "Student123!" },
            new { Email = "emma.wilson@lms.com", FirstName = "Emma", LastName = "Wilson", Password = "Student123!" }
        };

        foreach (var studentData in students)
        {
            var student = await userManager.FindByEmailAsync(studentData.Email);
            if (student == null)
            {
                student = new User
                {
                    UserName = studentData.Email,
                    Email = studentData.Email,
                    FirstName = studentData.FirstName,
                    LastName = studentData.LastName,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };
                var result = await userManager.CreateAsync(student, studentData.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(student, "Student");
                }
            }
        }

        // Seed courses
        if (!await context.Courses.AnyAsync())
        {
            var courses = new[]
            {
                new Course
                {
                    Id = Guid.NewGuid(),
                    Title = "Introduction to Programming",
                    Description = "Learn the basics of programming with this comprehensive course.",
                    Price = 49.99m,
                    InstructorId = (await userManager.FindByEmailAsync("john.doe@lms.com")).Id,
                    ImageUrl = "https://via.placeholder.com/150",
                    CreatedAt = DateTime.UtcNow,
                    IsPublished = true,
                    Category = "Programming",
                    Level = CourseLevel.Beginner,
                    Duration = 30,
                    Status = 0,
                    Requirements = "No prior experience required",
                    LearningObjectives = "Understand basic programming concepts",
                    Prerequisites = new List<Guid>(),
                    TargetAudience = "Beginners",
                    Tags = new List<string> { "programming", "beginner", "coding" }
                },
                new Course
                {
                    Id = Guid.NewGuid(),
                    Title = "Web Development Fundamentals",
                    Description = "Master the basics of web development including HTML, CSS, and JavaScript.",
                    Price = 69.99m,
                    InstructorId = (await userManager.FindByEmailAsync("sarah.smith@lms.com")).Id,
                    ImageUrl = "https://via.placeholder.com/150",
                    CreatedAt = DateTime.UtcNow,
                    IsPublished = true,
                    Category = "Web Development",
                    Level = CourseLevel.Beginner,
                    Duration = 45,
                    Status = 0,
                    Requirements = "Basic computer skills",
                    LearningObjectives = "Build responsive websites",
                    Prerequisites = new List<Guid>(),
                    TargetAudience = "Aspiring web developers",
                    Tags = new List<string> { "web", "html", "css", "javascript" }
                }
            };

            context.Courses.AddRange(courses);
            await context.SaveChangesAsync();

            // Add course media
            foreach (var course in courses)
            {
                var media = new Media
                {
                    Id = Guid.NewGuid(),
                    FileName = $"{course.Title.ToLower().Replace(" ", "-")}-image.jpg",
                    ContentType = "Image",
                    Url = "https://via.placeholder.com/150",
                    EntityId = course.Id,
                    EntityType = "Course",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                context.Media.Add(media);
            }
            await context.SaveChangesAsync();

            // Add lessons for each course
            foreach (var course in courses)
            {
                var lessons = new[]
                {
                    new Lesson
                    {
                        Id = Guid.NewGuid(),
                        Title = "Getting Started",
                        Description = "Introduction to the course",
                        Content = "Welcome to the course! In this lesson, we'll cover the basics.",
                        Order = 1,
                        CourseId = course.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsPublished = true,
                        Status = 0,
                        Type = LessonType.Reading,
                        Duration = 30
                    },
                    new Lesson
                    {
                        Id = Guid.NewGuid(),
                        Title = "Core Concepts",
                        Description = "Understanding the fundamental concepts",
                        Content = "Let's dive into the core concepts of the subject.",
                        Order = 2,
                        CourseId = course.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsPublished = true,
                        Status = 0,
                        Type = LessonType.Video,
                        Duration = 45
                    },
                    new Lesson
                    {
                        Id = Guid.NewGuid(),
                        Title = "Practice Exercise",
                        Description = "Test your knowledge",
                        Content = "Time to practice what you've learned!",
                        Order = 3,
                        CourseId = course.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsPublished = true,
                        Status = 0,
                        Type = LessonType.Quiz,
                        Duration = 60
                    }
                };

                context.Lessons.AddRange(lessons);
                await context.SaveChangesAsync();

                // Add exercises and questions for each lesson
                foreach (var lesson in lessons)
                {
                    if (lesson.Type == LessonType.Quiz)
                    {
                        var exercise = new Exercise
                        {
                            Id = Guid.NewGuid(),
                            Title = $"{lesson.Title} Quiz",
                            Content = "Test your understanding of the material",
                            Order = 1,
                            LessonId = lesson.Id,
                            CreatedAt = DateTime.UtcNow,
                            IsPublished = true,
                            Status = 0,
                            Type = ExerciseType.MultipleChoice,
                            TimeLimit = 30,
                            PassingScore = 70
                        };

                        context.Exercises.Add(exercise);
                        await context.SaveChangesAsync();

                        var questions = new[]
                        {
                            new Question
                            {
                                Id = Guid.NewGuid(),
                                ExerciseId = exercise.Id,
                                LessonId = lesson.Id,
                                Text = "What is the main purpose of this course?",
                                Type = ExerciseType.MultipleChoice,
                                Order = 1,
                                CreatedAt = DateTime.UtcNow
                            },
                            new Question
                            {
                                Id = Guid.NewGuid(),
                                ExerciseId = exercise.Id,
                                LessonId = lesson.Id,
                                Text = "Which of the following is a key concept covered?",
                                Type = ExerciseType.MultipleChoice,
                                Order = 2,
                                CreatedAt = DateTime.UtcNow
                            }
                        };

                        context.Questions.AddRange(questions);
                        await context.SaveChangesAsync();

                        // Add options for each question
                        foreach (var question in questions)
                        {
                            var options = new[]
                            {
                                new Option
                                {
                                    Id = Guid.NewGuid(),
                                    Text = "Option 1",
                                    IsCorrect = true,
                                    Order = 1,
                                    QuestionId = question.Id,
                                    CreatedAt = DateTime.UtcNow
                                },
                                new Option
                                {
                                    Id = Guid.NewGuid(),
                                    Text = "Option 2",
                                    IsCorrect = false,
                                    Order = 2,
                                    QuestionId = question.Id,
                                    CreatedAt = DateTime.UtcNow
                                },
                                new Option
                                {
                                    Id = Guid.NewGuid(),
                                    Text = "Option 3",
                                    IsCorrect = false,
                                    Order = 3,
                                    QuestionId = question.Id,
                                    CreatedAt = DateTime.UtcNow
                                }
                            };

                            context.Options.AddRange(options);
                        }
                        await context.SaveChangesAsync();
                    }
                }

                // Add course enrollments
                var enrolledStudents = await userManager.GetUsersInRoleAsync("Student");
                foreach (var student in enrolledStudents)
                {
                    var enrollment = new CourseProgress
                    {
                        Id = Guid.NewGuid(),
                        CourseId = course.Id,
                        UserId = student.Id,
                        LearningDate = DateTime.UtcNow,
                        Status = 0
                    };

                    context.CourseProgress.Add(enrollment);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}