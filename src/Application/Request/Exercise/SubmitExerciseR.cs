using MediatR;
using Application.Common.Reponses;

public class SubmitExerciseR : IRequest<SingleResponse>
{
    public Guid ExerciseId { get; set; }
    public Guid UserId { get; set; }
    public List<QuestionSubmission> QuestionSubmissions { get; set; }
}

public class QuestionSubmission
{
    public Guid QuestionId { get; set; }
    public bool IsCorrect { get; set; }
}
