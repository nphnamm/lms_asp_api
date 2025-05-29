using MediatR;
using Domain.Enums;
using Infrastructure.Data;
using Domain.Entities;
using Application.Request.Exercise;
using Application.Common.Reponses;
using Microsoft.EntityFrameworkCore;

namespace Application.Excercises.Commands;

public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public CreateExerciseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(CreateExerciseR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();

        // Verify lesson exists
        var lesson = await _context.Lessons.FindAsync(new object[] { request.LessonId }, cancellationToken);
        if (lesson == null)
            return res.SetError("Lesson not found");

        // Set default order if not provided
        if (request.Order == 0)
        {
            var lastExercise = await _context.Exercises
                .Where(e => e.LessonId == request.LessonId)
                .OrderByDescending(e => e.Order)
                .FirstOrDefaultAsync(cancellationToken);
            
            request.Order = lastExercise?.Order + 1 ?? 1;
        }

        var exercise = Exercise.Create(
            request.LessonId,
            request.Title,
            request.Content,
            request.Order,
            request.IsPublished,
            request.Type,
            0, // Initial status
            request.TimeLimit ?? 0,
            request.PassingScore ?? 0,
            request.RetryLimit ?? 0,
            request.AllowPartialCredit ?? false,
            request.Feedback ?? null,
            request.Instructions ?? null,
            request.Weight ?? 0,
            request.IsGraded ?? false,
            request.ShowAnswers ?? false,
            request.DueDate ?? DateTime.UtcNow,
            request.Hints ?? null,
            0, // Initial average score
            0  // Initial attempt count
        );

        _context.Exercises.Add(exercise);
        await _context.SaveChangesAsync(cancellationToken);

        return res.SetSuccess(exercise.ToViewDto());
    }
} 