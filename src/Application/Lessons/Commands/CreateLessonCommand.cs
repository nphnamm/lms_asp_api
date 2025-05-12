using MediatR;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;

public class CreateLessonCommand : IRequest<Guid>
{
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
    public LessonType Type { get; set; }
    
    // For question-based lessons
    public List<QuestionDto> Questions { get; set; }
    public string CorrectAnswer { get; set; }
    public List<OptionDto> Options { get; set; }
}