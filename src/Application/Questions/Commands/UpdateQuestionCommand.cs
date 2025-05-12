using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Questions.Commands;

public class UpdateQuestionCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public int Order { get; set; }
}

public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdateQuestionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FindAsync(new object[] { request.Id }, cancellationToken);

        if (question == null)
            return false;

        question.Text = request.Text;
        question.Type = request.Type;
        question.Order = request.Order;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
} 