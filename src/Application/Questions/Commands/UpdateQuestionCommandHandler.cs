using MediatR;
using Infrastructure.Data;

using Application.Common.Reponses;
namespace Application.Questions.Commands;

public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public UpdateQuestionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(UpdateQuestionR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var question = await _context.Questions.FindAsync(new object[] { request.Id }, cancellationToken);

        if (question == null)
            return res.SetError("Question not found");

        question.Text = request.Text;
        question.Type = request.Type;
        question.Order = request.Order;

        await _context.SaveChangesAsync(cancellationToken);
        return res.SetSuccess(true);
    }
} 