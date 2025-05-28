using System;
using Domain.Enums;

namespace Domain.Entities;

public partial class Lesson
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int Status { get; set; }
    public bool IsPublished { get; set; }
    public bool IsDeleted { get; set; } = false;
    
    // New fields
    public int Duration { get; set; }  // Duration in minutes
    public string? VideoUrl { get; set; }
    public string? Resources { get; set; }  // JSON string of additional resources
    public List<string> Keywords { get; set; } = new List<string>();
    public decimal CompletionRate { get; set; }
    public int ViewCount { get; set; }
    public string? Notes { get; set; }  // Instructor notes
    public bool IsPreview { get; set; }  // Whether this lesson is available for preview
    public LessonType Type { get; set; }
    
    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    public ICollection<Media> Media { get; set; } = new List<Media>();
    public ICollection<LessonAnalytics> LessonAnalytics { get; set; } = new List<LessonAnalytics>();
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    
}

public enum LessonType
{
    Video,
    Reading,
    Quiz,
    Assignment,
    Discussion,
    CodeExercise,
    Mixed
}