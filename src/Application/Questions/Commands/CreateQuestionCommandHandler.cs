using MediatR;
using Application.Common.Models;
using System.Collections.Generic;
using Infrastructure.Data;
using Domain.Entities;
using Application.Common.Reponses;
namespace Application.Questions.Commands;


public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public CreateQuestionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(CreateQuestionR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var question = new Question
        {
            Id = Guid.NewGuid(),
            LessonId = request.LessonId,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow
        };

        if (request.Options != null && request.Options.Count > 0)
        {
            foreach (var o in request.Options)
            {
                var option = new Option
                {
                    Id = Guid.NewGuid(),
                    Text = o.Text,
                    IsCorrect = o.IsCorrect,
                    QuestionId = question.Id,
                    Order = 0,
                    CreatedAt = DateTime.UtcNow
                };
                question.Options.Add(option);
            }
        }

        _context.Questions.Add(question);
        await _context.SaveChangesAsync(cancellationToken);
        return res.SetSuccess(question);
    }
} 