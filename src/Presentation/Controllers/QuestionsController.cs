using Application.Questions.Commands;
using Application.Questions.Queries;
using Application.Lessons.Queries;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Application.Courses.Queries;

namespace Presentation.Controllers;

[ApiController]
[Route("api/questions")]
[Authorize]
public class QuestionsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<QuestionsController> _logger;

    public QuestionsController(IMediator mediator, ILogger<QuestionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("lesson/{lessonId}")]
    public async Task<IActionResult> GetLessonQuestions(Guid lessonId)
    {
        // Get lesson to check access
        var lessonQuery = new GetLessonR { Id = lessonId };
        var lessonResult = await _mediator.Send(lessonQuery);
        
        if (lessonResult == null)
            return NotFound();

        var query = new GetLessonQuestionsR { LessonId = lessonId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestion(Guid id)
    {
        var query = new GetQuestionR { Id = id };
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> CreateQuestion(CreateQuestionR command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("multiple")]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> CreateMultipleQuestions(CreateMultipleQuestionsCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> UpdateQuestion(Guid id, UpdateQuestionR command)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> DeleteQuestion(Guid id)
    {
        var command = new DeleteQuestionR { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}