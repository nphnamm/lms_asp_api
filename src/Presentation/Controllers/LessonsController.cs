using System;
using System.Threading.Tasks;
using Application.Lessons.Commands;
using Application.Lessons.Queries;
using Application.Courses.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Models;
using Application.Request.Lesson;

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
    public async Task<IActionResult> GetCourseLessons(Guid courseId, [FromQuery] bool includeUnpublished = false)
    {
        // Only instructors can see unpublished lessons
        if (includeUnpublished && !IsInstructor)
            return Forbid();

        var query = new GetCourseLessonsR 
        { 
            CourseId = courseId,
            IncludeUnpublished = includeUnpublished
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLesson(Guid id)
    {
        var query = new GetLessonR { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> CreateLesson(CreateLessonR command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> UpdateLesson(Guid id, UpdateLessonR command)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> DeleteLesson(Guid id)
    {
        var command = new DeleteLessonR { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
