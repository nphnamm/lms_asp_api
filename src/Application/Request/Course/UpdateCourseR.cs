using MediatR;
using Application.Common.Reponses;

namespace Application.Request.Course;
public class UpdateCourseR : IRequest<SingleResponse>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsPublished { get; set; }
}
