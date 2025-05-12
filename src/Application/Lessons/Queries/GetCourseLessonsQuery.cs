using MediatR;
using Application.Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Lessons.Queries;

public class GetCourseLessonsQuery : IRequest<List<LessonDto>>
{
    public Guid CourseId { get; set; }
    public bool IncludeUnpublished { get; set; } = false;
}

public class GetCourseLessonsQueryHandler : IRequestHandler<GetCourseLessonsQuery, List<LessonDto>>
{
    private readonly ApplicationDbContext _context;

    public GetCourseLessonsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<LessonDto>> Handle(GetCourseLessonsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Lessons
            .Where(l => l.CourseId == request.CourseId)
            .AsQueryable();

        if (!request.IncludeUnpublished)
        {
            query = query.Where(l => l.IsPublished);
        }

        var lessons = await query
            .OrderBy(l => l.Order)
            .ToListAsync(cancellationToken);

        return lessons.Select(lesson => new LessonDto
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Content = lesson.Content,
            Type = lesson.Type,
            CourseId = lesson.CourseId,
            CreatedAt = lesson.CreatedAt,
            UpdatedAt = lesson.UpdatedAt
        }).ToList();
    }
}