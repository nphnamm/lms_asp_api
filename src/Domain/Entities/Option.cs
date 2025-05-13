using System;

namespace Domain.Entities;

public partial class Option
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
    public int Order { get; set; } // Added Order property
    public Guid QuestionId { get; set; }
    public Question Question { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}