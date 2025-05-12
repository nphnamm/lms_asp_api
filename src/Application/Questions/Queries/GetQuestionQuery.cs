using MediatR;
using Application.Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Questions.Queries;

public class GetQuestionQuery : IRequest<QuestionDto>
{
    public Guid Id { get; set; }
}

public class GetQuestionQueryHandler : IRequestHandler<GetQuestionQuery, QuestionDto>
{
    private readonly ApplicationDbContext _context;

    public GetQuestionQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<QuestionDto> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions
            .Include(q => q.Options)
            .FirstOrDefaultAsync(q => q.Id == request.Id, cancellationToken);

        if (question == null)
            return null;

        return new QuestionDto
        {
            Id = question.Id,
            LessonId = question.LessonId,
            Text = question.Text,
            Type = question.Type,
            Order = question.Order,
            Options = question.Options.Select(o => new OptionDto
            {
                Id = o.Id,
                Text = o.Text,
                IsCorrect = o.IsCorrect,
                Order = o.Order
            }).ToList()
        };
    }
} 