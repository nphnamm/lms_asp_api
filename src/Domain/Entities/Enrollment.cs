using System;

namespace Domain.Entities;

public class Enrollment
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid StudentId { get; set; }
    public User Student { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public EnrollmentStatus Status { get; set; }
    public decimal? Grade { get; set; }
}

public enum EnrollmentStatus
{
    Active,
    Completed,
    Dropped
} 