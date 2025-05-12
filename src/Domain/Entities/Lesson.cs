using System;
using Domain.Enums;

namespace Domain.Entities;

public class Lesson
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsPublished { get; set; }
    public int Type { get; set; }

    // Depending on lesson type, these may be used
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<Option> Options { get; set; } = new List<Option>();
}