using MediatR;
using Application.Common.Reponses;

namespace Application.Request.Lesson;
public class DeleteLessonR : IRequest<SingleResponse>
{
    public Guid Id { get; set; }
}