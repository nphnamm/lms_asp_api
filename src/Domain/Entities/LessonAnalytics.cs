using System;

namespace Domain.Entities;

public partial class LessonAnalytics
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; }
    public DateTime Date { get; set; }
    
    // View metrics
    public int TotalViews { get; set; }
    public int UniqueViews { get; set; }
    public int PreviewViews { get; set; }
    public int EnrolledViews { get; set; }
    
    // Engagement metrics
    public int TotalTimeSpent { get; set; }  // Total time spent in minutes
    public decimal AverageTimeSpent { get; set; }  // Average time spent per view
    public int TotalInteractions { get; set; }  // Clicks, scrolls, etc.
    public decimal CompletionRate { get; set; }  // Percentage of lesson completed
    
    // Exercise metrics
    public int TotalExerciseAttempts { get; set; }
    public int SuccessfulExerciseAttempts { get; set; }
    public decimal AverageExerciseScore { get; set; }
    public int AverageAttemptsPerExercise { get; set; }
    
    // Drop-off metrics
    public int DropOffCount { get; set; }  // Number of users who left before completing
    public decimal DropOffRate { get; set; }  // Percentage of users who dropped off
    public string? MostCommonDropOffPoint { get; set; }  // Where users most commonly leave
    
    // Feedback metrics
    public int HelpRequests { get; set; }  // Number of help requests
    public int ReportedIssues { get; set; }  // Number of reported issues
    public decimal UserSatisfaction { get; set; }  // User satisfaction score
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
} 