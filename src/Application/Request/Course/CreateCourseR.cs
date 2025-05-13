
using MediatR;
using Application.Common.Reponses;

namespace Application.Request.Course;
public class CreateCourseR : IRequest<SingleResponse>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid InstructorId { get; set; }
}
