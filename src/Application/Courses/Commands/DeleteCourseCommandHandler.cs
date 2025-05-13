using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Request.Course;
using Application.Common.Reponses;

namespace Application.Courses.Commands;



public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public DeleteCourseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(DeleteCourseR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var course = await _context.Courses.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (course == null)
            return res.SetError("Course not found");

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync(cancellationToken);
        return res.SetSuccess(true);
    }
} 