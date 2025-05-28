using MediatR;
using Application.Common.Models;
using System.Collections.Generic;
using Infrastructure.Data;
using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Domain.Enums;

namespace Application.Questions.Commands;

public class CreateMultipleQuestionsCommand : IRequest<List<Guid>>
{
    public Guid LessonId { get; set; }

    public ExerciseType ExerciseType { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
}

public class MultipleQuestionsDto
{
    public string Text { get; set; }
    public List<CreateOptionDto> Options { get; set; } = new();
}

public class CreateMultipleQuestionsCommandHandler : IRequestHandler<CreateMultipleQuestionsCommand, List<Guid>>
{
    private readonly ApplicationDbContext _context;

    public CreateMultipleQuestionsCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guid>> Handle(CreateMultipleQuestionsCommand request, CancellationToken cancellationToken)
    {
        var createdQuestionIds = new List<Guid>();
        var questions = new List<Question>();

        foreach (var questionDto in request.Questions)
        {
            var question = new Question
            {
                Id = Guid.NewGuid(),
                LessonId = request.LessonId,
                Text = questionDto.Text,
                Type = request.ExerciseType,
                CreatedAt = DateTime.UtcNow,
                Order = request.Questions.IndexOf(questionDto)
            };

            if (questionDto.Options != null && questionDto.Options.Count > 0)
            {
                foreach (var optionDto in questionDto.Options)
                {
                    var option = new Option
                    {
                        Id = Guid.NewGuid(),
                        Text = optionDto.Text,
                        IsCorrect = optionDto.IsCorrect,
                        QuestionId = question.Id,
                        Order = questionDto.Options.IndexOf(optionDto),
                        CreatedAt = DateTime.UtcNow
                    };
                    question.Options.Add(option);
                }
            }

            questions.Add(question);
            createdQuestionIds.Add(question.Id);
        }

        await _context.Questions.AddRangeAsync(questions, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return createdQuestionIds;
    }
}