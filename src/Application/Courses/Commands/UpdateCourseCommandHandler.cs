using MediatR;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Request.Course;
using Application.Common.Reponses;

namespace Application.Courses.Commands;


public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseR, SingleResponse>
{
    private readonly ApplicationDbContext _context;

    public UpdateCourseCommandHandler(ApplicationDbContext context)
    {   
        _context = context;
    }

    public async Task<SingleResponse> Handle(UpdateCourseR request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (course == null)
            return new SingleResponse().SetError("Course not found");

        course.Title = request.Title;
        course.Description = request.Description;
        course.Price = request.Price;
        course.IsPublished = request.IsPublished;
        course.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return new SingleResponse().SetSuccess(true);
    }
} 