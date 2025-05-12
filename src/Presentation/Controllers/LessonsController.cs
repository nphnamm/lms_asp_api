using System;
using System.Threading.Tasks;
using Application.Lessons.Commands;
using Application.Lessons.Queries;
using Application.Courses.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/lessons")]
[Authorize]
public class LessonsController : BaseController
{
    private readonly IMediator _mediator;

    public LessonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("course/{courseId}")]
    public async Task<ActionResult<List<LessonDto>>> GetCourseLessons(Guid courseId, [FromQuery] bool includeUnpublished = false)
    {
        // Get course to check if user has access
        var course = await _mediator.Send(new GetCourseQuery { Id = courseId });
        if (course == null)
            return NotFound();

        // Only instructors can see unpublished lessons
        if (includeUnpublished && !IsInstructor)
            return Forbid();

        // Check if user is the instructor or an enrolled student
        if (!IsInstructor && course.InstructorId != CurrentUserId)
        {
            // TODO: Add check for student enrollment
            // For now, only allow access to published lessons
            includeUnpublished = false;
        }

        var query = new GetCourseLessonsQuery
        {
            CourseId = courseId,
            IncludeUnpublished = includeUnpublished
        };
        var lessons = await _mediator.Send(query);
        return Ok(lessons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LessonDto>> GetLesson(Guid id)
    {
        var query = new GetLessonQuery { Id = id };
        var lesson = await _mediator.Send(query);

        if (lesson == null)
            return NotFound();

        // Get course to check access
        var course = await _mediator.Send(new GetCourseQuery { Id = lesson.CourseId });
        if (course == null)
            return NotFound();

        // Only instructors can see unpublished lessons
        if (!lesson.IsPublished && !IsInstructor)
            return Forbid();

        // Check if user is the instructor or an enrolled student
        if (!IsInstructor && course.InstructorId != CurrentUserId)
        {
            // TODO: Add check for student enrollment
            // For now, only allow access to published lessons
            if (!lesson.IsPublished)
                return Forbid();
        }

        return Ok(lesson);
    }

    [HttpPost]
    [Authorize(Roles = "Instructor")]
    public async Task<ActionResult<Guid>> CreateLesson(CreateLessonCommand command)
    {
        // Get course to check ownership
        var course = await _mediator.Send(new GetCourseQuery { Id = command.CourseId });
        if (course == null)
            return NotFound();

        // Only the course owner can add lessons
        if (course.InstructorId != CurrentUserId)
            return Forbid();

        var lessonId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetLesson), new { id = lessonId }, lessonId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLesson(Guid id, UpdateLessonCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        // Get lesson to check course ownership
        var lesson = await _mediator.Send(new GetLessonQuery { Id = id });
        if (lesson == null)
            return NotFound();

        // Get course to check ownership
        var course = await _mediator.Send(new GetCourseQuery { Id = lesson.CourseId });
        if (course == null)
            return NotFound();

        // Only instructors can update lessons
        if (!IsInstructor)
            return Forbid();

        // Only the course owner can update lessons
        if (course.InstructorId != CurrentUserId)
            return Forbid();

        var success = await _mediator.Send(command);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(Guid id)
    {
        // Get lesson to check course ownership
        var lesson = await _mediator.Send(new GetLessonQuery { Id = id });
        if (lesson == null)
            return NotFound();

        // Get course to check ownership
        var course = await _mediator.Send(new GetCourseQuery { Id = lesson.CourseId });
        if (course == null)
            return NotFound();

        // Only instructors can delete lessons
        if (!IsInstructor)
            return Forbid();

        // Only the course owner can delete lessons
        if (course.InstructorId != CurrentUserId)
            return Forbid();

        var command = new DeleteLessonCommand { Id = id };
        var success = await _mediator.Send(command);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
