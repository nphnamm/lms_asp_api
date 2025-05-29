using MediatR;
using Domain.Enums;
using Infrastructure.Data;
using Domain.Entities;
using Application.Request.Lesson;
using Application.Common.Reponses;
using Microsoft.EntityFrameworkCore;

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
        if (request.Order == 0)
        {
            var lastLesson = await _context.Lessons.Where(l => l.CourseId == request.CourseId).OrderByDescending(l => l.Order).FirstOrDefaultAsync();
            if (lastLesson != null)
            {
                request.Order = lastLesson.Order + 1;
            }
        }
        else
        {
            var lastLesson = await _context.Lessons.Where(l => l.CourseId == request.CourseId && l.Order == request.Order).FirstOrDefaultAsync();
            if (lastLesson != null)
            {
                res.SetError("Order is not valid");
                return res;
            }
        }
        var lesson = Lesson.Create(
            request.CourseId,
            request.Title,
            request.Description,
            request.Content,
            request.Order,
            request.IsPublished,
            0,
            (LessonType)request.Type,
            request.Duration,
            request.VideoUrl,
            request.Resources,
            request.Keywords,
            request.CompletionRate ?? 0,
            request.ViewCount ?? 0,
            request.Notes,
            request.IsPreview
        );
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync(cancellationToken);
        return res.SetSuccess(lesson.ToViewDto());
    }
}