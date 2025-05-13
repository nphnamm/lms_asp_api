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
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            InstructorId = request.InstructorId,
            CreatedAt = DateTime.UtcNow,
            IsPublished = false
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);

        return res.SetSuccess(course);
    }
}