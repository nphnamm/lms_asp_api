using Application.Questions.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Application.Request.Question;
using Application.Request.Exercise;
using Application.Common.Reponses;
using Domain.Entities;
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
        // Get exercise to check access and get exercise details
        var exerciseQuery = new GetExerciseR { Id = exerciseId };
        var exerciseResult = await _mediator.Send(exerciseQuery);
        
        if (exerciseResult == null || !exerciseResult.Succeeded)
            return NotFound();

        var query = new GetExerciseQuestionsR { ExerciseId = exerciseId };
        var result = await _mediator.Send(query);
        
        if (result == null || !result.Succeeded)
            return NotFound();

        // Extract exercise data
        var exercise = exerciseResult.Data as Exercise.ViewDto;
        if (exercise == null)
            return NotFound();

        // Create response with exercise information and questions
        var response = new SingleResponse();
        var responseData = new
        {
            typeExercise = exercise.Type.ToString(),
            exerciseId = exercise.Id,
            exerciseName = exercise.Title,
            wordBank = exercise.WordBank,
            data = result.Data
        };

        return Ok(response.SetSuccess(responseData));
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