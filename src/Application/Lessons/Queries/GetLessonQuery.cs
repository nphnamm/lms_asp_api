using MediatR;
using Application.Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Common.Reponses;
namespace Application.Lessons.Queries;



public class GetLessonQueryHandler : IRequestHandler<GetLessonR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public GetLessonQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(GetLessonR request, CancellationToken cancellationToken)
    {
        var lesson = await _context.Lessons
            .Include(l => l.Questions)
            .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

        if (lesson == null)
            return new SingleResponse().SetError("Lesson not found");

        return new SingleResponse().SetSuccess(lesson.ToViewDto());

    }
}