using MediatR;
using Application.Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Common.Reponses;
namespace Application.Lessons.Queries;


public class GetCourseLessonsQueryHandler : IRequestHandler<GetCourseLessonsR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public GetCourseLessonsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(GetCourseLessonsR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var lessons = await _context.Lessons
            .Where(l => l.CourseId == request.CourseId)
            .OrderBy(l => l.Order)
            .ToListAsync(cancellationToken);

        return res.SetSuccess(lessons.Select(l => l.ToViewDto()).ToList());
    }
}