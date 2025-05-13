using MediatR;
using Application.Common.Reponses;

namespace Application.Request.Lesson;
public class UpdateLessonR : IRequest<SingleResponse>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
    public bool IsPublished { get; set; }
    public int Type { get; set; }
    public string CorrectAnswer { get; set; }
}