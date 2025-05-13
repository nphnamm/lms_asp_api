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
    public async Task<ActionResult<List<QuestionDto>>> GetLessonQuestions(Guid lessonId)
    {
        // Get lesson to check access
        var lesson = await _mediator.Send(new GetLessonR { Id = lessonId });
        if (lesson == null)
            return NotFound();

        // Get course to check access
        // var course = await _mediator.Send(new GetCourseQuery { Id = lesson.CourseId });
        // if (course == null)
        //     return NotFound();

        // Only instructors and course owners can see questions for unpublished lessons
        // if (!lesson.IsPublished && !IsInstructor && course.InstructorId != CurrentUserId)
        //     return Forbid();

        // For published lessons, allow access to instructors, course owners, and enrolled students
        // if (!IsInstructor && course.InstructorId != CurrentUserId)
        // {
        //     // TODO: Add check for student enrollment
        //     // For now, only allow access to questions in published lessons
        //     if (!lesson.IsPublished)
        //         return Forbid();
        // }

        var query = new GetLessonQuestionsR { LessonId = lessonId };
        var questions = await _mediator.Send(query);
        return Ok(questions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<QuestionDto>> GetQuestion(Guid id)
    {
        var query = new GetQuestionR { Id = id };
        var question = await _mediator.Send(query);

        if (question == null)
            return NotFound();

        // Get lesson to check access
        // var lesson = await _mediator.Send(new GetLessonR { Id = question.Data.LessonId });
        // if (lesson == null)
        //     return NotFound();

        // Get course to check access
        // var course = await _mediator.Send(new GetCourseR { Id = lesson.CourseId });
        // if (course == null)
        //     return NotFound();

        // Only instructors and course owners can see questions for unpublished lessons
        // if (!lesson.IsPublished && !IsInstructor && course.InstructorId != CurrentUserId)
        //     return Forbid();

        // For published lessons, allow access to instructors, course owners, and enrolled students
        // if (!IsInstructor && course.InstructorId != CurrentUserId)
        // {
        //     // TODO: Add check for student enrollment
        //     // For now, only allow access to questions in published lessons
        //     if (!lesson.IsPublished)
        //         return Forbid();
        // }

        return Ok(question);
    }

    [HttpPost]
    [Authorize(Roles = "Instructor")]
    public async Task<ActionResult<Guid>> CreateQuestion(CreateQuestionR command)
    {
        _logger.LogInformation("User ID: {UserId}, IsInstructor: {IsInstructor}", CurrentUserId, IsInstructor);
        _logger.LogInformation("User claims: {Claims}", string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}")));

        // Get lesson to check course ownership
        var lesson = await _mediator.Send(new GetLessonR { Id = command.LessonId });
        if (lesson == null)
            return NotFound();

        // Get course to check ownership
        // var course = await _mediator.Send(new GetCourseR { Id = lesson.CourseId });
        // if (course == null)
        //     return NotFound();

        // Only the course owner can add questions
        // if (course.InstructorId != CurrentUserId)
        //     return Forbid();

        var questionId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetQuestion), new { id = questionId }, questionId);
    }

    [HttpPost("multiple")]
    [Authorize(Roles = "Instructor")]
    public async Task<ActionResult<List<Guid>>> CreateMultipleQuestions(CreateMultipleQuestionsCommand command)
    {
        _logger.LogInformation("User ID: {UserId}, IsInstructor: {IsInstructor}", CurrentUserId, IsInstructor);
        _logger.LogInformation("User claims: {Claims}", string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}")));

        // Get lesson to check course ownership
        var lesson = await _mediator.Send(new GetLessonR { Id = command.LessonId });
        if (lesson == null)
            return NotFound();

        // Get course to check ownership
        // var course = await _mediator.Send(new GetCourseR { Id = lesson.CourseId });
        // if (course == null)
        //     return NotFound();

        // Only the course owner can add questions
        // if (course.InstructorId != CurrentUserId)
        //     return Forbid();

        var questionIds = await _mediator.Send(command);
        return Ok(questionIds);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> UpdateQuestion(Guid id, UpdateQuestionR command)
    {
        if (id != command.Id)
            return BadRequest();

        // Get question to check lesson ownership
        var question = await _mediator.Send(new GetQuestionR { Id = id });
        if (question == null)
            return NotFound();

        // Get lesson to check course ownership
        // var lesson = await _mediator.Send(new GetLessonR { Id = question.Data.LessonId });
        // if (lesson == null)
        //     return NotFound();

        // Get course to check ownership
        // var course = await _mediator.Send(new GetCourseR { Id = lesson.CourseId });
        // if (course == null)
        //     return NotFound();

        // Only the course owner can update questions
        // if (course.InstructorId != CurrentUserId)
        //     return Forbid();

        var success = await _mediator.Send(command);
        if (!success.Succeeded)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> DeleteQuestion(Guid id)
    {
        // Get question to check lesson ownership
        var question = await _mediator.Send(new GetQuestionR { Id = id });
        if (question == null)
            return NotFound();

        // Get lesson to check course ownership
        // var lesson = await _mediator.Send(new GetLessonR { Id = question.Data.LessonId });
        // if (lesson == null)
        //     return NotFound();

        // Get course to check ownership
        // var course = await _mediator.Send(new GetCourseR { Id = lesson.CourseId });
        // if (course == null)
        //     return NotFound();

        // Only the course owner can delete questions
        // if (course.InstructorId != CurrentUserId)
        //     return Forbid();

        var command = new DeleteQuestionR { Id = id };
        var success = await _mediator.Send(command);
        if (!success.Succeeded)
            return NotFound();

        return NoContent();
    }
}