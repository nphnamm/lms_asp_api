
namespace Domain.Entities;

public partial class QuestionHistory
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public Question Question { get; set; }
    public Guid ExerciseHistoryId { get; set; }
    public ExerciseHistory ExerciseHistory { get; set; }
    public DateTime AnsweredAt { get; set; }
    public bool IsCorrect { get; set; }
    public int Status { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
