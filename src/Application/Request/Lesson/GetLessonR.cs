using MediatR;
using Application.Common.Reponses;

public class GetLessonR : IRequest<SingleResponse>
{
    public Guid Id { get; set; }

    public bool IncludeExercise { get; set; }
}