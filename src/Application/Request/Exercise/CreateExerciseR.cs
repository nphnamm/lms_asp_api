using MediatR;
using Application.Common.Reponses;
using Domain.Enums;

namespace Application.Request.Exercise;

public class CreateExerciseR : IRequest<SingleResponse>
{
    public string Title { get; set; }
    public string? Content { get; set; }
    public int Order { get; set; }
    public Guid LessonId { get; set; }
    public bool IsPublished { get; set; }
    public ExerciseType Type { get; set; }
    public int? TimeLimit { get; set; }
    public decimal? PassingScore { get; set; }
    public int? RetryLimit { get; set; }
    public bool? AllowPartialCredit { get; set; }
    public string? Feedback { get; set; }
    public string? Instructions { get; set; }
    public decimal? Weight { get; set; }
    public bool? IsGraded { get; set; }
    public bool? ShowAnswers { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Hints { get; set; }
}