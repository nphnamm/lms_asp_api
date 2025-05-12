using System;
using System.Threading.Tasks;
using Application.Courses.Commands.CreateCourse;
using Application.Courses.Queries.GetCourses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var query = new GetCoursesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCourses), new { id = result }, result);
    }
} 