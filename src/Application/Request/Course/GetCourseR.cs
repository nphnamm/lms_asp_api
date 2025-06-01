using MediatR;
using Application.Common.Reponses;

public class GetCourseR : IRequest<SingleResponse>
{
    public Guid Id { get; set; }
    public bool IncludeLessons { get; set; }
}