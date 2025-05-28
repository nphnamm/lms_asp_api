using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public partial class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    
    // New fields
    public string? ProfilePicture { get; set; }
    public string? Bio { get; set; }
    public string? Education { get; set; }
    public string? Qualifications { get; set; }
    public string? Skills { get; set; }  // JSON string of skills
    public string? Preferences { get; set; }  // JSON string of user preferences
    public string? TimeZone { get; set; }
    public string? Language { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public int CompletedCourses { get; set; }
    public decimal AverageScore { get; set; }
    public string? SocialLinks { get; set; }  // JSON string of social media links
    public bool IsInstructor { get; set; }
    public string? InstructorBio { get; set; }
    public string? InstructorSpecialties { get; set; }  // JSON string of specialties
    public decimal InstructorRating { get; set; }
    public int TotalStudents { get; set; }
    
    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<CourseProgress> CourseProgress { get; set; } = new List<CourseProgress>();
}
