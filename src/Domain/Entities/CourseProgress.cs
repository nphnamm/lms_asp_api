using System;

namespace Domain.Entities;

public partial class CourseProgress
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsCompleted { get; set; } = false;
    public DateTime LearningDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public CourseProgressStatus Status { get; set; }
    
    // New fields
    public DateTime? LastAccessedAt { get; set; }
    public decimal ProgressPercentage { get; set; }
    public string? Notes { get; set; }  // User's personal notes
    public decimal? Rating { get; set; }  // User's rating of the course
    public string? Review { get; set; }  // User's review of the course
    public DateTime? CompletionDate { get; set; }
    public int TimeSpent { get; set; }  // Total time spent in minutes
    public decimal? Score { get; set; }  // Overall score in the course
    public string? CertificateUrl { get; set; }
    public bool IsFavorite { get; set; }
    public string? LearningPath { get; set; }  // JSON string of completed lessons/exercises
    public DateTime? ExpiryDate { get; set; }  // For time-limited access
    public string? Feedback { get; set; }  // Instructor feedback
}

public enum CourseProgressStatus
{
    NotStarted,
    InProgress,
    Completed,
    Dropped,
    OnHold,
    Failed
} 