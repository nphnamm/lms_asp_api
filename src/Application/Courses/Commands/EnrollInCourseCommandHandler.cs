using MediatR;
using Application.Common.Reponses;
using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
public class EnrollInCourseCommandHandler : IRequestHandler<EnrollInCourseR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public EnrollInCourseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(EnrollInCourseR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var course = await _context.Courses.FindAsync(request.CourseId);
        if (course == null)
            return res.SetError("Course not found");

        var courseProgress = CourseProgress.Create(request.CourseId, request.UserId, DateTime.UtcNow, CourseProgressStatus.InProgress, false);
        await _context.CourseProgress.AddAsync(courseProgress, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return res.SetSuccess(courseProgress.ToViewDto());
    }
}
