using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Common.Reponses;
using Domain.Entities;
namespace Application.Excercises.Commands;

public class SubmitExerciseCommandHandler : IRequestHandler<SubmitExerciseR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public SubmitExerciseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(SubmitExerciseR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();

        // Get exercise and validate
        var exercise = await _context.Exercises
            .Include(e => e.Questions)
                .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(e => e.Id == request.ExerciseId, cancellationToken);

        if (exercise == null)
            return res.SetError("Exercise not found");

        // Create exercise history
        var exerciseHistory = ExerciseHistory.Create(exercise.Id, request.UserId, DateTime.UtcNow, DateTime.UtcNow, 0);

        await _context.ExerciseHistories.AddAsync(exerciseHistory, cancellationToken);

        var score = 0;
        // Create question histories
        foreach (var questionSubmission in request.QuestionSubmissions)
        {
            var question = exercise.Questions.FirstOrDefault(q => q.Id == questionSubmission.QuestionId);
            
            if (question == null) continue;

            var history = QuestionHistory.Create(question.Id, exerciseHistory.Id, questionSubmission.IsCorrect);

            await _context.QuestionHistories.AddAsync(history, cancellationToken);

            if (questionSubmission.IsCorrect)
                score+=10;
        }

        exerciseHistory.Update(score, 1, false);

        await _context.SaveChangesAsync(cancellationToken);

        return res.SetSuccess(exerciseHistory.ToViewDto());
    }
}
