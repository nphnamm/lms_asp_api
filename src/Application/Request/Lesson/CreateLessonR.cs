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
    public string Quiz { get; set; }
    public List<string> Tags { get; set; }
    public decimal Rating { get; set; }
    public int TotalEnrollments { get; set; }
    public string Notes { get; set; }
    public bool IsPreview { get; set; }
}
