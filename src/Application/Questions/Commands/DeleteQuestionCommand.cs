using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Questions.Commands;

public class DeleteQuestionCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeleteQuestionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FindAsync(new object[] { request.Id }, cancellationToken);

        if (question == null)
            return false;

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
} 