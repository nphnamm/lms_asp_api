using MediatR;
using Application.Common.Reponses;

namespace Application.Request.Exercise;

public class DeleteExerciseR : IRequest<SingleResponse>
{
    public Guid Id { get; set; }
}