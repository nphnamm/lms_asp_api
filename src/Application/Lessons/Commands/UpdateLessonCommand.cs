using MediatR;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Request.Lesson;
using Application.Common.Reponses;

namespace Application.Lessons.Commands;



public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonR, SingleResponse>    
{
    private readonly ApplicationDbContext _context;

    public UpdateLessonCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(UpdateLessonR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var lesson = await _context.Lessons.FindAsync(new object[] { request.Id }, cancellationToken);

        if (lesson == null)
            return res.SetError("Lesson not found");

        lesson.Update(request.Title, request.Content, request.Order, request.IsPublished);

        await _context.SaveChangesAsync(cancellationToken);
        return res.SetSuccess(lesson.ToViewDto());
    }
}