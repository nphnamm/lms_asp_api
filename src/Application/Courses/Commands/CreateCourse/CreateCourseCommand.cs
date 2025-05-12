using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Courses.Commands.CreateCourse;

public record CreateCourseCommand : IRequest<Guid>
{
    public string Title { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
}

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public CreateCourseCommandHandler(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            CreatedAt = DateTime.UtcNow,
            IsPublished = false
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);

        return course.Id;
    }
} 