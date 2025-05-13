using System;
using System.Threading.Tasks;
using Application.Courses.Commands;
using Application.Courses.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Models;
using Application.Request.Course;
using Application.Common.Reponses;
namespace Presentation.Controllers;

[Authorize]
public class CoursesController : BaseController
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses([FromQuery] bool includeUnpublished = true)
    {
        // Only instructors can see unpublished courses
        if (includeUnpublished && !IsInstructor)
            return Forbid();

        var query = new GetCoursesR { IncludeUnpublished = includeUnpublished };
        var courses = await _mediator.Send(query);
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(Guid id)
    {
        var query = new GetCourseR { Id = id };
        var course = await _mediator.Send(query);

        if (course == null)
            return NotFound();


        return Ok(course);
    }

    [HttpPost]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> CreateCourse(CreateCourseR command)
    {
        // Set the instructor ID from the current user
        command.InstructorId = CurrentUserId ?? throw new UnauthorizedAccessException("User ID not found");
        var course = await _mediator.Send(command);

        return Ok(course);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCourse(Guid id, UpdateCourseR command)
    {
        var res = new SingleResponse();
        if (id != command.Id)
            res.SetError("Invalid course ID");


        // Get the course to check ownership
        var course = await _mediator.Send(new GetCourseR { Id = id });
        if (course == null)
            return NotFound();

        // Only instructors can update courses  
        if (!IsInstructor)
            return Forbid();


        var success = await _mediator.Send(command);
        if (!success.Succeeded)
            return NotFound();

        return Ok(success);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        // Get the course to check ownership
        var course = await _mediator.Send(new GetCourseR { Id = id });
        if (course == null)
            return NotFound();

        // Only instructors can delete courses
        if (!IsInstructor)
            return Forbid();

        var command = new DeleteCourseR { Id = id };
        var success = await _mediator.Send(command);
        if (!success.Succeeded)
            return NotFound();

        return Ok(success);
    }
}