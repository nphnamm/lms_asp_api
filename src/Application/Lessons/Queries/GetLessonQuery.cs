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
        if (request.IncludeExercise)
        {
            var lesson = await _context.Lessons
                .Include(l => l.Exercises)
                .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (lesson == null)
                return new SingleResponse().SetError("Lesson not found");

            return new SingleResponse().SetSuccess(lesson.ToViewDto());
        }
        else
        {
            var lesson = await _context.Lessons
                .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (lesson == null)
                return new SingleResponse().SetError("Lesson not found");

            return new SingleResponse().SetSuccess(lesson.ToViewDto());
        }
    }
}