using Domain.Enums;
using Application.Common.Models;

namespace Presentation.Models;

public class CreateLessonRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int Order { get; set; }
    public LessonType Type { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
    public List<OptionDto> Options { get; set; } = new();
    public string CorrectAnswer { get; set; }
} 