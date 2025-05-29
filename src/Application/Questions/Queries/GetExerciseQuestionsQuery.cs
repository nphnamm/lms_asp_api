using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using Application.Common.Reponses;
using Application.Request.Question;

namespace Application.Questions.Queries;


public class GetExerciseQuestionsQueryHandler : IRequestHandler<GetExerciseQuestionsR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public GetExerciseQuestionsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(GetExerciseQuestionsR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var questions = await _context.Questions
            .Include(q => q.Options)
            .Where(q => q.ExerciseId == request.ExerciseId)
            .OrderBy(q => q.Order)
            .ToListAsync(cancellationToken);

        return res.SetSuccess(questions.Select(q => q.ToViewDto()).ToList());
    }
} 