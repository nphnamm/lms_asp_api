using MediatR;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.Commands;

public class UpdateCourseCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsPublished { get; set; }
}

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdateCourseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (course == null)
            return false;

        course.Title = request.Title;
        course.Description = request.Description;
        course.Price = request.Price;
        course.IsPublished = request.IsPublished;
        course.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
} 