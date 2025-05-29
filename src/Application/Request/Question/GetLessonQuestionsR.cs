using MediatR;
using Application.Common.Reponses;

public class GetExerciseQuestionsR : IRequest<SingleResponse>
{
    public Guid ExerciseId { get; set; }
}
