using Domain.Entities;

namespace Domain.Entities;

partial class Option
{

    public static Option Create(Guid questionId, string text, bool isCorrect, int order)
    {
        return new Option
        {
            Id = Guid.NewGuid(),
            QuestionId = questionId,
            Text = text,
            IsCorrect = isCorrect,
            Order = order,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string text, bool isCorrect, int order)
    {
        Text = text;
        IsCorrect = isCorrect;
        Order = order;
        UpdatedAt = DateTime.UtcNow;
    }

    public T ToBaseDto<T>() where T : BaseDto, new()
    {
        return new T
        {
            Id = Id,
            Text = Text,
            IsCorrect = IsCorrect,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt ?? DateTime.UtcNow
        };
    }

    public class BaseDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 