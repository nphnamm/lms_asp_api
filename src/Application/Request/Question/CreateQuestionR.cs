using MediatR;
using Application.Common.Reponses;

public class CreateQuestionR : IRequest<SingleResponse>
{
    public Guid LessonId { get; set; }
    public string Text { get; set; }
    public List<CreateOptionDto> Options { get; set; } = new();
}

public class CreateOptionDto
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
} 