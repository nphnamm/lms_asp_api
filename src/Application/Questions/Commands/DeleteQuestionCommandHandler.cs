using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Reponses;
namespace Application.Questions.Commands;



public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public DeleteQuestionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(DeleteQuestionR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var question = await _context.Questions.FindAsync(new object[] { request.Id }, cancellationToken);

        if (question == null)
            return res.SetError("Question not found");

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync(cancellationToken);
        return res.SetSuccess(true);
    }
} 