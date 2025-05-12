using MediatR;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Data;

namespace Application.Courses.Commands;



public class CreateCourseCommand : IRequest<CourseDto>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid InstructorId { get; set; }
}

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseDto>
{
    private readonly ApplicationDbContext _context;

    public CreateCourseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
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

        return new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Price = course.Price,
            InstructorId = course.InstructorId,
            CreatedAt = course.CreatedAt,
            IsPublished = course.IsPublished
        };
    }
}