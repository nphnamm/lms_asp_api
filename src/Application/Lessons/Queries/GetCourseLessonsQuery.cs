using MediatR;
using Application.Common.Models;
using Domain.Entities;

public class GetCourseLessonsQuery : IRequest<List<LessonDto>>
{
    public Guid CourseId { get; set; }
}