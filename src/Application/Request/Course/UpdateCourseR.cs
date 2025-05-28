using MediatR;
using Application.Common.Reponses;
using Microsoft.AspNetCore.Http;
namespace Application.Request.Course;
public class UpdateCourseR : IRequest<SingleResponse>
{
    public Guid Id { get; set; }
    public string Title { get; set; }   
    public string Description { get; set; }
    public decimal? Price { get; set; }
    public bool? IsPublished { get; set; }
    public string Level { get; set; }
    public IFormFile? Image { get; set; }
    public int? Status { get; set; }
    public string? Category { get; set; }
    public List<string>? Tags { get; set; }
    public List<Guid>? Prerequisites { get; set; }
    public decimal? Rating { get; set; }
    public int? TotalEnrollments { get; set; }
    public string? Syllabus { get; set; }
    public string LearningObjectives { get; set; }
    public string Requirements { get; set; }
    public string TargetAudience { get; set; }
}
