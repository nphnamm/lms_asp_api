using MediatR;
using Application.Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Common.Reponses;
namespace Application.Questions.Queries;


public class GetQuestionQueryHandler : IRequestHandler<GetQuestionR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public GetQuestionQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(GetQuestionR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var question = await _context.Questions
            .Include(q => q.Options)
            .FirstOrDefaultAsync(q => q.Id == request.Id, cancellationToken);

        if (question == null)
            return new SingleResponse().SetError("Question not found");

        return new SingleResponse().SetSuccess(question.ToViewDto());
    }
} 