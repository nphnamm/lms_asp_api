using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Question
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; }
    public ICollection<Option> Options { get; set; } = new List<Option>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}