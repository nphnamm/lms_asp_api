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
        if(request.Id == Guid.Empty)
            return res.SetError("Invalid lesson ID");
        var lesson = await _context.Lessons.FindAsync(new object[] { request.Id }, cancellationToken);

        if (lesson == null)
            return res.SetError("Lesson not found");
        var title = request.Title ?? lesson.Title;
        var content = request.Content ?? lesson.Content;
        var order = request.Order ?? lesson.Order;
        var isPublished = request.IsPublished ?? lesson.IsPublished;
        var status = request.Status ?? lesson.Status;
        var type = request.Type ?? (int)lesson.Type;
        var duration = request.Duration ?? lesson.Duration;
        var videoUrl = request.VideoUrl ?? lesson.VideoUrl;
        var resources = request.Resources ?? lesson.Resources;
        var keywords = request.Keywords ?? lesson.Keywords;
        var completionRate = request.CompletionRate ?? lesson.CompletionRate;
        var viewCount = request.ViewCount ?? lesson.ViewCount;
        var notes = request.Notes ?? lesson.Notes;
        var isPreview = request.IsPreview ?? lesson.IsPreview;

        lesson.Update(
            title, 
            content, 
            order, 
            isPublished, 
            status, 
            (LessonType)type, 
            duration, 
            videoUrl, 
            resources, 
            keywords, 
            completionRate, 
            viewCount, 
            notes, 
            isPreview
        );

        await _context.SaveChangesAsync(cancellationToken);
        return res.SetSuccess(lesson.ToViewDto());
    }
}