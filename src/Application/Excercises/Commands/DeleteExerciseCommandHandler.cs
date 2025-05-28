using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Request.Exercise;
using Application.Common.Reponses;

namespace Application.Excercises.Commands;

public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public DeleteExerciseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(DeleteExerciseR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var exercise = await _context.Exercises.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (exercise == null)
            return res.SetError("Exercise not found");

        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync(cancellationToken);
        return res.SetSuccess(true);
    }
} 