using MediatR;
using Application.Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Common.Reponses;
namespace Application.Lessons.Queries;



public class GetLessonQueryHandler : IRequestHandler<GetLessonR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public GetLessonQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(GetLessonR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var lesson = await _context.Lessons
            .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

        if (lesson == null)
            return res.SetError("Lesson not found");

        return res.SetSuccess(lesson);

    }
}