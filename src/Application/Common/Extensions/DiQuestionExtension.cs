

using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Application.Request.Course;
using Application.Common.Reponses;
using Application.Questions.Commands;
using Application.Questions.Queries;
using Application.Request;
public static class DiQuestionExtension
{
    #region -- Methods --

    /// <summary>
    /// Add DI for include commands and queries
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddDiQuestion(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddQuestionCommands(life);
        p.AddQuestionQueries(life);
    }

    /// <summary>
    /// Add commands handler
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddQuestionCommands(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddBehavior<IRequestHandler<CreateQuestionR, SingleResponse>, CreateQuestionCommandHandler>(life);
        p.AddBehavior<IRequestHandler<UpdateQuestionR, SingleResponse>, UpdateQuestionCommandHandler>(life);
        p.AddBehavior<IRequestHandler<DeleteQuestionR, SingleResponse>, DeleteQuestionCommandHandler>(life);
    }

    /// <summary>
    /// Add queries handler
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddQuestionQueries(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddBehavior<IRequestHandler<GetQuestionR, SingleResponse>, GetQuestionQueryHandler>(life);
        p.AddBehavior<IRequestHandler<GetLessonQuestionsR, SingleResponse>, GetLessonQuestionsQueryHandler>(life);
    }

    #endregion
}
