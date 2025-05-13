using MediatR;
using Application.Common.Reponses;

public class DeleteQuestionR : IRequest<SingleResponse>
{
    public Guid Id { get; set; }
}