using MediatR;
using Application.Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Lessons.Queries;

public class GetLessonQuery : IRequest<LessonDto>
{
    public Guid Id { get; set; }
}

public class GetLessonQueryHandler : IRequestHandler<GetLessonQuery, LessonDto>
{
    private readonly ApplicationDbContext _context;

    public GetLessonQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LessonDto> Handle(GetLessonQuery request, CancellationToken cancellationToken)
    {
        var lesson = await _context.Lessons
            .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

        if (lesson == null)
            return null;

        return new LessonDto
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Content = lesson.Content,
            Type = lesson.Type,
            CourseId = lesson.CourseId,
            CreatedAt = lesson.CreatedAt,
            UpdatedAt = lesson.UpdatedAt,
            IsPublished = lesson.IsPublished
        };
    }
}