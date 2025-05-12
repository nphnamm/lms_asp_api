using Domain.Enums;
using Application.Common.Models;

namespace Presentation.Models;

public class CreateLessonRequest
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public int Order { get; set; }
    public LessonType Type { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
    public List<OptionDto> Options { get; set; } = new();
    public required string CorrectAnswer { get; set; }
} 