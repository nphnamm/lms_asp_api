using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using Application.Common.Reponses;

namespace Application.Questions.Queries;


public class GetLessonQuestionsQueryHandler : IRequestHandler<GetLessonQuestionsR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public GetLessonQuestionsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(GetLessonQuestionsR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var questions = await _context.Questions
            .Include(q => q.Options)
            .Where(q => q.LessonId == request.LessonId)
            .OrderBy(q => q.Order)
            .ToListAsync(cancellationToken);

        return res.SetSuccess(questions);
    }
} 