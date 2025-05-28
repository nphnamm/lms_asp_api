using MediatR;
using Application.Common.Reponses;
using Microsoft.AspNetCore.Http;
using Domain.Entities;
namespace Application.Request.Course;
public class CreateCourseR : IRequest<SingleResponse>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid InstructorId { get; set; }
    public IFormFile? Image { get; set; }
    public int Level { get; set;     }
    public CourseLevel Category { get; set; }
    public List<string>? Tags { get; set; }
    public List<Guid>? Prerequisites { get; set; }
    public decimal Rating { get; set; }
    public int TotalEnrollments { get; set; }
    public string? Syllabus { get; set; }
    public string? LearningObjectives { get; set; }
    public string? Requirements { get; set; }
    public string? TargetAudience { get; set; }
}
