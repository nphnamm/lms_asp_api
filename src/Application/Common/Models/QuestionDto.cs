using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Application.Common.Models;

public class QuestionDto
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public int Order { get; set; }
    public List<OptionDto> Options { get; set; }
}

