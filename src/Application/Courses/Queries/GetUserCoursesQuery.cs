using MediatR;
using Application.Common.Models;
using Domain.Entities;

public class GetUserCoursesQuery : IRequest<List<CourseDto>>
{
    public Guid UserId { get; set; }
}