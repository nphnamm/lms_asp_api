using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities;

public partial class Question
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; }
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; }
    public ExerciseType Type { get; set; }
    public int Order { get; set; }
    public ICollection<Option> Options { get; set; } = new List<Option>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<QuestionHistory> QuestionHistories { get; set; } = new List<QuestionHistory>();
}