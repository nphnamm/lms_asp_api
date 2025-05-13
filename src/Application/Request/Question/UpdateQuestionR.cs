using MediatR;
using Domain.Enums;
using Application.Common.Reponses;

public class UpdateQuestionR : IRequest<SingleResponse>
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public LessonType Type { get; set; }
    public int Order { get; set; }
}

