using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Request.Lesson;
using Application.Common.Reponses;

namespace Application.Lessons.Commands;



public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public DeleteLessonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(DeleteLessonR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var lesson = await _context.Lessons.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (lesson == null)
            return res.SetError("Lesson not found");

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync(cancellationToken);
        return res.SetSuccess(true);
    }
} 