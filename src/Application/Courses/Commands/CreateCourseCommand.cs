using MediatR;
using Application.Common.Models;
using Domain.Entities;

public class CreateCourseCommand : IRequest<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid CreatedBy { get; set; }
}