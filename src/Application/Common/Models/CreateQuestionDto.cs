using System.Collections.Generic;

namespace Application.Common.Models;

public class CreateQuestionDto
{
    public string Text { get; set; }
    public List<CreateOptionDto> Options { get; set; } = new();
} 