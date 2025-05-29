using Application.Common.Reponses;
using MediatR;

public class EnrollInCourseR : IRequest<SingleResponse>
{
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }
}
