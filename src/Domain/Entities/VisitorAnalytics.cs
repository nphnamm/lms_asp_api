using System;

namespace Domain.Entities;

public partial class VisitorAnalytics
{
    public Guid Id { get; set; }
    public DateTime VisitDate { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
    public string? ReferrerUrl { get; set; }
    public string? PageUrl { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? DeviceType { get; set; }
    public string? Browser { get; set; }
    public string? OperatingSystem { get; set; }
    public Guid? UserId { get; set; }  // Null if visitor is not logged in
    public User? User { get; set; }
    public Guid? CourseId { get; set; }  // Null if not visiting a course
    public Course? Course { get; set; }
    public Guid? LessonId { get; set; }  // Null if not viewing a lesson
    public Lesson? Lesson { get; set; }
    public int TimeSpent { get; set; }  // Time spent in seconds
    public bool IsUniqueVisit { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public enum DeviceType
{
    Desktop,
    Mobile,
    Tablet,
    Other
} 