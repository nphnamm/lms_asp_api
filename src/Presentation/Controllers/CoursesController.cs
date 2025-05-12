using System;
using System.Threading.Tasks;
using Application.Courses.Commands;
using Application.Courses.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Models;

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
    public async Task<ActionResult<List<CourseDto>>> GetCourses([FromQuery] bool includeUnpublished = false)
    {
        // Only instructors can see unpublished courses
        if (includeUnpublished && !IsInstructor)
            return Forbid();

        var query = new GetCoursesQuery { IncludeUnpublished = includeUnpublished };
        var courses = await _mediator.Send(query);
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourse(Guid id)
    {
        var query = new GetCourseQuery { Id = id };
        var course = await _mediator.Send(query);

        if (course == null)
            return NotFound();

        // Only instructors can see unpublished courses
        if (!course.IsPublished && !IsInstructor)
            return Forbid();

        return Ok(course);
    }

    [HttpPost]
    [Authorize(Roles = "Instructor")]
    public async Task<ActionResult<GeneralServiceResponseDto<CourseDto>>> CreateCourse(CreateCourseCommand command)
    {
        // Set the instructor ID from the current user
        command.InstructorId = CurrentUserId ?? throw new UnauthorizedAccessException("User ID not found");
        var course = await _mediator.Send(command);

        return Ok(GeneralServiceResponseDto<CourseDto>.Success(course, "Course created successfully"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(Guid id, UpdateCourseCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        // Get the course to check ownership
        var course = await _mediator.Send(new GetCourseQuery { Id = id });
        if (course == null)
            return NotFound();

        // Only instructors can update courses
        if (!IsInstructor)
            return Forbid();

        // Only the course owner can update it
        if (course.InstructorId != CurrentUserId)
            return Forbid();

        var success = await _mediator.Send(command);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        // Get the course to check ownership
        var course = await _mediator.Send(new GetCourseQuery { Id = id });
        if (course == null)
            return NotFound();

        // Only instructors can delete courses
        if (!IsInstructor)
            return Forbid();

        // Only the course owner can delete it
        if (course.InstructorId != CurrentUserId)
            return Forbid();

        var command = new DeleteCourseCommand { Id = id };
        var success = await _mediator.Send(command);
        if (!success)
            return NotFound();

        return NoContent();
    }
}