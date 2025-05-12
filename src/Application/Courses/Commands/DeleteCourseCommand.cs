using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.Commands;

public class DeleteCourseCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeleteCourseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (course == null)
            return false;

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
} 