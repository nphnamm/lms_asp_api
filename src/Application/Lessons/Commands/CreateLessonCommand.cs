using MediatR;
using Domain.Enums;
using Infrastructure.Data;
using Domain.Entities;
using Application.Request.Lesson;
using Application.Common.Reponses;
using Domain.Entities;

namespace Application.Lessons.Commands;



public class CreateLessonCommandHandler : IRequestHandler<CreateLessonR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public CreateLessonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(CreateLessonR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var lesson = Lesson.Create(request.CourseId, request.Title, request.Content, request.Order, request.IsPublished, (LessonType)request.Type);

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync(cancellationToken);
        return res.SetSuccess(lesson.ToViewDto());
    }
}