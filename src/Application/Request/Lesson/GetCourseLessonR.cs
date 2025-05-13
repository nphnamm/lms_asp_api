using MediatR;
using Application.Common.Reponses;

public class GetCourseLessonsR : IRequest<SingleResponse>
{
    public Guid CourseId { get; set; }
    public bool IncludeUnpublished { get; set; } = false;
}
