namespace Domain.Entities;

public partial class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid InstructorId { get; set; }
    public User Instructor { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int Status { get; set; }
    public bool IsPublished { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string? ImageUrl { get; set; }
    
    // New fields
    public int Duration { get; set; }  // Duration in minutes
    public CourseLevel Level { get; set; }
    public string Category { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public List<Guid> Prerequisites { get; set; } = new List<Guid>();
    public decimal Rating { get; set; }
    public int TotalEnrollments { get; set; }
    public string? Syllabus { get; set; }
    public string? LearningObjectives { get; set; }
    public string? Requirements { get; set; }
    public string? TargetAudience { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
    public ICollection<CourseProgress> CourseProgress { get; set; }
    public ICollection<Media> Media { get; set; }
    public ICollection<CourseAnalytics> CourseAnalytics { get; set; }
}

public enum CourseLevel
{
    Beginner,
    Intermediate,
    Advanced
}