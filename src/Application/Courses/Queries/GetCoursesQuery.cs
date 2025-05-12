using MediatR;
using Application.Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.Queries;

public class GetCoursesQuery : IRequest<List<CourseDto>>
{
    public bool IncludeUnpublished { get; set; } = false;
}

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<CourseDto>>
{
    private readonly ApplicationDbContext _context;

    public GetCoursesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Courses
            .Include(c => c.Instructor)
            .AsQueryable();

        if (!request.IncludeUnpublished)
        {
            query = query.Where(c => c.IsPublished);
        }

        var courses = await query.ToListAsync(cancellationToken);

        return courses.Select(course => new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Price = course.Price,
            InstructorId = course.InstructorId,
            IsPublished = course.IsPublished,
            CreatedAt = course.CreatedAt,
            UpdatedAt = course.UpdatedAt
        }).ToList();
    }
} 