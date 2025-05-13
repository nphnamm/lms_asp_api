using MediatR;
using Application.Common.Models;
using Application.Common.Reponses;

public class GetCourseR : IRequest<SingleResponse>
{
    public Guid Id { get; set; }
}