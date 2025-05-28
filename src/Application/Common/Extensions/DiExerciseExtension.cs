using Application.Common.Reponses;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Application.Lessons.Commands;
using Application.Request.Lesson;
using Application.Lessons.Queries;
using Application.Request.Exercise;
using Application.Excercises.Commands;
using Application.Excercises.Queries;
namespace Application.Common.Extensions;


/// <summary>
/// DI extension
/// </summary>
public static class DiExerciseExtension
{
    #region -- Methods --

    /// <summary>
    /// Add DI for include commands and queries
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddDiExercise(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddExerciseCommands(life);
        p.AddExerciseQueries(life);
    }

    /// <summary>
    /// Add commands handler
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddExerciseCommands(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddBehavior<IRequestHandler<CreateExerciseR, SingleResponse>, CreateExerciseCommandHandler>(life);
        p.AddBehavior<IRequestHandler<UpdateExerciseR, SingleResponse>, UpdateExerciseCommandHandler>(life);
        p.AddBehavior<IRequestHandler<DeleteExerciseR, SingleResponse>, DeleteExerciseCommandHandler>(life);
    }

    /// <summary>
    /// Add queries handler
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddExerciseQueries(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddBehavior<IRequestHandler<GetExerciseR, SingleResponse>, GetExerciseQueryHandler>(life);
        p.AddBehavior<IRequestHandler<GetLessonExercisesR, SingleResponse>, GetLessonExercisesQueryHandler>(life);
    }

    #endregion
}
