using MediatR;
using Domain.Enums;
using Infrastructure.Data;
using Domain.Entities;
using Application.Request.Lesson;
using Application.Common.Reponses;

namespace Application.Lessons.Commands;



public class CreateLessonCommandHandler : IRequestHandler<CreateLessonR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public CreateLessonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(CreateLessonR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
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
        return res.SetSuccess(lesson);
    }
}