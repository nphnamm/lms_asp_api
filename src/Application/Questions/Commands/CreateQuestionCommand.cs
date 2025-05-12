using MediatR;
using Application.Common.Models;
using System.Collections.Generic;
using Infrastructure.Data;
using Domain.Entities;

namespace Application.Questions.Commands;

public class CreateQuestionCommand : IRequest<Guid>
{
    public Guid LessonId { get; set; }
    public string Text { get; set; }
    public List<CreateOptionDto> Options { get; set; } = new();
}

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Guid>
{
    private readonly ApplicationDbContext _context;

    public CreateQuestionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
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
        return question.Id;
    }
} 