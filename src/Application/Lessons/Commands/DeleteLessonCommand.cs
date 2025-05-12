using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Lessons.Commands;

public class DeleteLessonCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeleteLessonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _context.Lessons.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (lesson == null)
            return false;

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
} 