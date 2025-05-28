using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Request.Exercise;
using Application.Common.Reponses;
using Domain.Entities;

namespace Application.Excercises.Commands;

public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public UpdateExerciseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(UpdateExerciseR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var exercise = await _context.Exercises.FindAsync(new object[] { request.Id }, cancellationToken);

        if (exercise == null)
            return res.SetError("Exercise not found");

        exercise.Update(
            request.Title,
            request.Content,
            request.Order,
            request.IsPublished,
            request.Type,
            0, // Status remains unchanged
            request.TimeLimit,
            request.PassingScore,
            request.RetryLimit,
            request.AllowPartialCredit,
            request.Feedback,
            request.Instructions,
            request.Weight,
            request.IsGraded,
            request.ShowAnswers,
            request.DueDate ?? DateTime.UtcNow,
            request.Hints,
            0, // AverageScore remains unchanged
            0  // AttemptCount remains unchanged
        );

        await _context.SaveChangesAsync(cancellationToken);
        return res.SetSuccess(exercise);
    }
} 