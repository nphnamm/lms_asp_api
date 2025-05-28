using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Common.Reponses;
using Application.Request.Exercise;

namespace Application.Excercises.Queries;

public class GetLessonExercisesQueryHandler : IRequestHandler<GetLessonExercisesR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public GetLessonExercisesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(GetLessonExercisesR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var exercises = await _context.Exercises
            .Include(e => e.Questions)
            .Include(e => e.Options)
            .Where(e => e.LessonId == request.LessonId)
            .OrderBy(e => e.Order)
            .ToListAsync(cancellationToken);

        return res.SetSuccess(exercises);
    }
} 