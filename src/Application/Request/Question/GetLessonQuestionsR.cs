using MediatR;
using Application.Common.Reponses;

public class GetLessonQuestionsR : IRequest<SingleResponse>
{
    public Guid LessonId { get; set; }
}
