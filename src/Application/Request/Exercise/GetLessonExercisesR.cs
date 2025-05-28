using MediatR;
using Application.Common.Reponses;

namespace Application.Request.Exercise;

public class GetLessonExercisesR : IRequest<SingleResponse>
{
    public Guid LessonId { get; set; }
} 