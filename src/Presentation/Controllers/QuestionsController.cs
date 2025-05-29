using Application.Questions.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Application.Request.Question;
using Application.Request.Exercise;
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

    [HttpGet("exercise/{exerciseId}")]
    public async Task<IActionResult> GetExerciseQuestions(Guid exerciseId)
    {
        // Get lesson to check access
        var exerciseQuery = new GetExerciseR { Id = exerciseId };
        var exerciseResult = await _mediator.Send(exerciseQuery);
        
        if (exerciseResult == null)
            return NotFound();

        var query = new GetExerciseQuestionsR { ExerciseId = exerciseId };
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