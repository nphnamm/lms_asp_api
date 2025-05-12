using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Models;

public class LessonDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public LessonType Type { get; set; }
    public Guid CourseId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
} 