using System;

namespace Domain.Entities;

public partial class CourseAnalytics
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime Date { get; set; }
    
    // View metrics
    public int TotalViews { get; set; }
    public int UniqueViews { get; set; }
    public int PreviewViews { get; set; }  // Views of preview content
    public int EnrolledViews { get; set; }  // Views by enrolled students
    
    // Engagement metrics
    public int TotalTimeSpent { get; set; }  // Total time spent in minutes
    public decimal AverageTimeSpent { get; set; }  // Average time spent per view
    public int TotalInteractions { get; set; }  // Clicks, scrolls, etc.
    public decimal CompletionRate { get; set; }  // Percentage of course completed
    
    // Enrollment metrics
    public int NewEnrollments { get; set; }
    public int CompletedEnrollments { get; set; }
    public int DroppedEnrollments { get; set; }
    public decimal ConversionRate { get; set; }  // Views to enrollment ratio
    
    // Revenue metrics
    public decimal Revenue { get; set; }
    public int Refunds { get; set; }
    public decimal NetRevenue { get; set; }
    
    // Performance metrics
    public decimal AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public int PositiveReviews { get; set; }
    public int NegativeReviews { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
} 