using System;
using Domain.Enums;

namespace Domain.Entities;

public partial class Exercise
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsPublished { get; set; }
    public int Status { get; set; }
    public ExerciseType Type { get; set; }
    
    // New fields
    public int TimeLimit { get; set; }  // Time limit in minutes
    public decimal PassingScore { get; set; }
    public int RetryLimit { get; set; }
    public bool AllowPartialCredit { get; set; }
    public string? Feedback { get; set; }  // General feedback for the exercise
    public string? Instructions { get; set; }
    public decimal Weight { get; set; }  // Weight of this exercise in the lesson
    public bool IsGraded { get; set; }
    public bool ShowAnswers { get; set; }  // Whether to show answers after submission
    public DateTime? DueDate { get; set; }
    public string? Hints { get; set; }  // JSON string of hints
    public string? WordBank { get; set; }  // Word bank for fill-in-the-blank exercises
    public decimal AverageScore { get; set; }
    public int AttemptCount { get; set; }
    
    // Depending on lesson type, these may be used
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<Option> Options { get; set; } = new List<Option>();
    public ICollection<ExerciseHistory> ExerciseHistories { get; set; } = new List<ExerciseHistory>();
    public ICollection<QuestionHistory> QuestionHistories { get; set; } = new List<QuestionHistory>();
}


