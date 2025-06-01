using Application.Excercises.Commands;
using Application.Excercises.Queries;
using Application.Lessons.Queries;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Application.Request.Exercise;

namespace Presentation.Controllers;

[ApiController]
[Route("api/exercises")]
[Authorize]
public class ExercisesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<ExercisesController> _logger;

    public ExercisesController(IMediator mediator, ILogger<ExercisesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("lesson/{lessonId}")]
    public async Task<IActionResult> GetLessonExercises(Guid lessonId)
    {
        // Get lesson to check access
        var lessonQuery = new GetLessonR { Id = lessonId };
        var lessonResult = await _mediator.Send(lessonQuery);
        
        if (lessonResult == null)
            return NotFound();

        var query = new GetLessonExercisesR { LessonId = lessonId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExercise(Guid id)
    {
        var query = new GetExerciseR { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> CreateExercise(CreateExerciseR command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> UpdateExercise(Guid id, [FromBody] UpdateExerciseR command)
    {


        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpDelete]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> DeleteExercise(DeleteExerciseR command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitExercise(SubmitExerciseR command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}