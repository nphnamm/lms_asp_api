using MediatR;
using Domain.Enums;
using Infrastructure.Data;
using Domain.Entities;

namespace Application.Lessons.Commands;

public class CreateLessonCommand : IRequest<Guid>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
    public Guid CourseId { get; set; }
    public bool IsPublished { get; set; }
    public int Type { get; set; }
}

public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, Guid>
{
    private readonly ApplicationDbContext _context;

    public CreateLessonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = new Lesson
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Content = request.Content,
            Order = request.Order,
            CourseId = request.CourseId,
            IsPublished = request.IsPublished,
            Type = request.Type,
            CreatedAt = DateTime.UtcNow
        };

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync(cancellationToken);
        return lesson.Id;
    }
}