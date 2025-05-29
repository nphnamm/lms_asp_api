using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Common.Reponses;
using Application.Request.Exercise;

namespace Application.Excercises.Queries;

public class GetExerciseQueryHandler : IRequestHandler<GetExerciseR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public GetExerciseQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(GetExerciseR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var exercise = await _context.Exercises
            .Include(e => e.Questions)
                .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (exercise == null)
            return res.SetError("Exercise not found");

        return res.SetSuccess(exercise.ToViewDto());
    }
} 