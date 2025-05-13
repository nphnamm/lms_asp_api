using MediatR;
using Application.Common.Reponses;

namespace Application.Request.Course;
public class DeleteCourseR : IRequest<SingleResponse>
{
    public Guid Id { get; set; }
}   