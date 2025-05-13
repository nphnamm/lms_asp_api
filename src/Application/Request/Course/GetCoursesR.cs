using MediatR;
using Application.Common.Reponses;

public class GetCoursesR : IRequest<SingleResponse>
{
    public bool IncludeUnpublished { get; set; } = false;
}