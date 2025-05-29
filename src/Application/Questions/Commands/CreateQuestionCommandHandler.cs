using MediatR;
using Infrastructure.Data;
using Domain.Entities;
using Application.Common.Reponses;
using Application.Request.Question;
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

        if (request.ExerciseId == Guid.Empty)
            return res.SetError("Invalid exercise ID");

        var exercise = await _context.Exercises.FindAsync(new object[] { request.ExerciseId }, cancellationToken);
        var lessonId = request.LessonId;
        if (exercise == null)
            return res.SetError("Exercise not found");

        if (request.Questions.Count == 1)
        {
            var question = Question.Create(request.ExerciseId, lessonId, request.Questions[0].Text, request.LessonType, 0);
            await _context.Questions.AddAsync(question, cancellationToken);

            foreach (var optionDto in request.Questions[0].Options)
            {
                var option = Option.Create(question.Id, optionDto.Text, optionDto.IsCorrect, 0);
                await _context.Options.AddAsync(option, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            var newExercise = await _context.Exercises.FindAsync(new object[] { request.ExerciseId }, cancellationToken);
            return res.SetSuccess(newExercise.ToViewDto());
        }
        else
        {

            foreach (var questionDto in request.Questions)
            {
                var question = Question.Create(request.ExerciseId, lessonId, questionDto.Text, request.LessonType, request.Questions.IndexOf(questionDto));
                await _context.Questions.AddAsync(question, cancellationToken);

                if (questionDto.Options != null && questionDto.Options.Count > 0)
                {
                    foreach (var optionDto in questionDto.Options)
                    {
                        var option = Option.Create(question.Id, optionDto.Text, optionDto.IsCorrect, questionDto.Options.IndexOf(optionDto));
                        await _context.Options.AddAsync(option, cancellationToken);
                    }

                }
            }
            await _context.SaveChangesAsync(cancellationToken);
            var newExercise = await _context.Exercises.FindAsync(new object[] { request.ExerciseId }, cancellationToken);
            return res.SetSuccess(newExercise.ToViewDto());
        }


    }
}