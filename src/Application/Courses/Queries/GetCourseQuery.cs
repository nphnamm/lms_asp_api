using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Common.Reponses;
using Domain.Entities;

namespace Application.Courses.Queries;



public class GetCourseQueryHandler : IRequestHandler<GetCourseR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public GetCourseQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(GetCourseR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var course = new Course();
        if (request.IncludeLessons)
        {
            course = await _context.Courses
               .Include(c => c.Instructor)
               .Include(c => c.Lessons)
               .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        }
        else
        {
            course = await _context.Courses
               .Include(c => c.Instructor)
               .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        }

        if (course == null)
            return res.SetError("Course not found");

        return res.SetSuccess(course.ToViewDto());
    }
}