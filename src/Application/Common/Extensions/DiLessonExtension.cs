using Application.Common.Reponses;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Application.Lessons.Commands;
using Application.Request.Lesson;
using Application.Lessons.Queries;
namespace Application.Common.Extensions;


/// <summary>
/// DI extension
/// </summary>
public static class DiLessonExtension
{
    #region -- Methods --

    /// <summary>
    /// Add DI for include commands and queries
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddDiLesson(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddLessonCommands(life);
        p.AddLessonQueries(life);
    }

    /// <summary>
    /// Add commands handler
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddLessonCommands(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddBehavior<IRequestHandler<CreateLessonR, SingleResponse>, CreateLessonCommandHandler>(life);
        p.AddBehavior<IRequestHandler<UpdateLessonR, SingleResponse>, UpdateLessonCommandHandler>(life);
        p.AddBehavior<IRequestHandler<DeleteLessonR, SingleResponse>, DeleteLessonCommandHandler>(life);
    }

    /// <summary>
    /// Add queries handler
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddLessonQueries(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddBehavior<IRequestHandler<GetLessonR, SingleResponse>, GetLessonQueryHandler>(life);
        p.AddBehavior<IRequestHandler<GetCourseLessonsR, SingleResponse>, GetCourseLessonsQueryHandler>(life);
    }

    #endregion
}
