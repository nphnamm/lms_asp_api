

using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Application.Request.Course;
using Application.Common.Reponses;
using Application.Courses.Commands;
using Application.Courses.Queries;
public static class DiCourseExtension
{
    #region -- Methods --

    /// <summary>
    /// Add DI for include commands and queries
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddDiCourse(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddCourseCommands(life);
        p.AddCourseQueries(life);
    }

    /// <summary>
    /// Add commands handler
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddCourseCommands(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddBehavior<IRequestHandler<CreateCourseR, SingleResponse>, CreateCourseCommandHandler>(life);
        p.AddBehavior<IRequestHandler<UpdateCourseR, SingleResponse>, UpdateCourseCommandHandler>(life);
        p.AddBehavior<IRequestHandler<DeleteCourseR, SingleResponse>, DeleteCourseCommandHandler>(life);

        p.AddBehavior<IRequestHandler<EnrollInCourseR, SingleResponse>, EnrollInCourseCommandHandler>(life);
    }

    /// <summary>
    /// Add queries handler
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddCourseQueries(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddBehavior<IRequestHandler<GetCourseR, SingleResponse>, GetCourseQueryHandler>(life);
        p.AddBehavior<IRequestHandler<GetCoursesR, SingleResponse>, GetCoursesQueryHandler>(life);
    }

    #endregion
}
