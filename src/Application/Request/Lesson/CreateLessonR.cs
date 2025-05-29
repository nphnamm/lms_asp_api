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
    public int Duration { get; set; }
    public string VideoUrl { get; set; }
    public string Resources { get; set; }
    public List<string> Keywords { get; set; }
    public decimal ?CompletionRate { get; set; }
    public int ?ViewCount { get; set; }
    public string ?Notes { get; set; }
    public bool IsPreview { get; set; }
}
