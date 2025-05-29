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

        if(request.Id == Guid.Empty)
            return res.SetError("Invalid exercise ID");

        var exercise = await _context.Exercises.FindAsync(new object[] { request.Id }, cancellationToken);

        if (exercise == null)
            return res.SetError("Exercise not found");
            
        var title = request.Title ?? exercise.Title;
        var content = request.Content ?? exercise.Content;
        var order = request.Order ?? exercise.Order;
        var isPublished = request.IsPublished ?? exercise.IsPublished;
        var status = request.Status ?? exercise.Status;
        var type = request.Type ?? exercise.Type;
        var timeLimit = request.TimeLimit ?? exercise.TimeLimit;
        var passingScore = request.PassingScore ?? exercise.PassingScore;
        var retryLimit = request.RetryLimit ?? exercise.RetryLimit;
        var allowPartialCredit = request.AllowPartialCredit ?? exercise.AllowPartialCredit;
        var feedback = request.Feedback ?? exercise.Feedback;
        var instructions = request.Instructions ?? exercise.Instructions;
        var weight = request.Weight ?? exercise.Weight;
        var isGraded = request.IsGraded ?? exercise.IsGraded;
        var showAnswers = request.ShowAnswers ?? exercise.ShowAnswers;
        var dueDate = request.DueDate ?? exercise.DueDate;
        var hints = request.Hints ?? exercise.Hints;
        var averageScore = request.AverageScore ?? exercise.AverageScore;
        var attemptCount = request.AttemptCount ?? exercise.AttemptCount;
        exercise.Update(
            title,
            content,    
            order,
            isPublished,
            type,
            status,
            timeLimit,
            passingScore,
            retryLimit,
            allowPartialCredit,
            feedback,
            instructions,
            weight,
            isGraded,
            showAnswers,
            dueDate ?? DateTime.UtcNow,
            hints,
            averageScore,
            attemptCount
        );

        await _context.SaveChangesAsync(cancellationToken);
        return res.SetSuccess(exercise);
    }
} 