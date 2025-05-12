using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Courses.Queries.GetCourses;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.Queries.GetCourses;

public record GetCoursesQuery : IRequest<IEnumerable<CourseDto>>;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IEnumerable<CourseDto>>
{
    private readonly ApplicationDbContext _context;

    public GetCoursesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _context.Courses
            .Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Price = c.Price,
                InstructorName = c.Instructor.FirstName + " " + c.Instructor.LastName,
                CreatedAt = c.CreatedAt,
                IsPublished = c.IsPublished
            })
            .ToListAsync(cancellationToken);

        return courses;
    }
}

public class CourseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string InstructorName { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPublished { get; set; }
} 