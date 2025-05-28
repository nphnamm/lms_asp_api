using MediatR;
using Application.Common.Reponses;

namespace Application.Request.Lesson;
public class CreateLessonR : IRequest<SingleResponse>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public Guid CourseId { get; set; }
    public bool IsPublished { get; set; }
    public int Type { get; set; }
}