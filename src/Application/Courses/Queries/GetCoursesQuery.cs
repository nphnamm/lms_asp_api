using MediatR;
using Application.Common.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Common.Reponses;
using Application.Request.Course;
namespace Application.Courses.Queries;



public class GetCoursesQueryHandler : IRequestHandler<GetCoursesR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public GetCoursesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(GetCoursesR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var query = _context.Courses
            .Include(c => c.Instructor)
            .AsQueryable();

        if (!request.IncludeUnpublished)
        {
            query = query.Where(c => c.IsPublished);
        }

        var courses = await query.ToListAsync(cancellationToken);

        return res.SetSuccess(courses.Select(c => c.ToViewDto()).ToList());
    }
} 