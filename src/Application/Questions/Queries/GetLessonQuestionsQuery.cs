using MediatR;
using Application.Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Questions.Queries;

public class GetLessonQuestionsQuery : IRequest<List<QuestionDto>>
{
    public Guid LessonId { get; set; }
}

public class GetLessonQuestionsQueryHandler : IRequestHandler<GetLessonQuestionsQuery, List<QuestionDto>>
{
    private readonly ApplicationDbContext _context;

    public GetLessonQuestionsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<QuestionDto>> Handle(GetLessonQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = await _context.Questions
            .Include(q => q.Options)
            .Where(q => q.LessonId == request.LessonId)
            .OrderBy(q => q.Order)
            .ToListAsync(cancellationToken);

        return questions.Select(q => new QuestionDto
        {
            Id = q.Id,
            LessonId = q.LessonId,
            Text = q.Text,
            Type = q.Type,
            Order = q.Order,
            Options = q.Options.Select(o => new OptionDto
            {
                Id = o.Id,
                Text = o.Text,
                IsCorrect = o.IsCorrect,
                Order = o.Order
            }).ToList()
        }).ToList();
    }
} 