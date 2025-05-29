using MediatR;
using Application.Common.Reponses;
using Domain.Enums;
using Application.Common.Models;

namespace Application.Request.Question;

public class CreateQuestionR : IRequest<SingleResponse>
{
    public Guid ExerciseId { get; set; }
    public Guid LessonId { get; set; }
    public ExerciseType LessonType { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
}

public class CreateOptionDto
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
} 