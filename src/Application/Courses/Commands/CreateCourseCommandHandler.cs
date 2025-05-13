using MediatR;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Data;
using Application.Request.Course;
using Application.Common.Reponses;

namespace Application.Courses.Commands;



public class CreateCourseCommandHandler : IRequestHandler<CreateCourseR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public CreateCourseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SingleResponse> Handle(CreateCourseR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        var course = Course.Create(request.InstructorId, request.Title, request.Description, request.Price, true);

        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);

        return res.SetSuccess(course);
    }
}