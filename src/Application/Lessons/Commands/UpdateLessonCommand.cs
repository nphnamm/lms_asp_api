using MediatR;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Lessons.Commands;

public class UpdateLessonCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
    public bool IsPublished { get; set; }
    public int Type { get; set; }
    public string CorrectAnswer { get; set; }
}

public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdateLessonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _context.Lessons.FindAsync(new object[] { request.Id }, cancellationToken);

        if (lesson == null)
            return false;

        lesson.Title = request.Title;
        lesson.Content = request.Content;
        lesson.Order = request.Order;
        lesson.IsPublished = request.IsPublished;
        lesson.Type = request.Type;
        lesson.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}