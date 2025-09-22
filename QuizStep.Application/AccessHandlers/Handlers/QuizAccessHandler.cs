using Microsoft.AspNetCore.Authorization;
using QuizStep.Application.AccessHandlers.Requirements;
using QuizStep.Core.Entities;
using QuizStep.Core.Enums;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.AccessHandlers.Handlers;

public class QuizAccessHandler : AuthorizationHandler<QuizAccessRequirement, Quiz>
{
    private readonly IUser _user;
    private readonly IQuizProvider _quizProvider;

    public QuizAccessHandler(IUser user, IQuizProvider quizProvider)
    {
        _user = user;
        _quizProvider = quizProvider;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        QuizAccessRequirement requirement, Quiz resource)
    {
        var user = await _user.GetUserAsync();
        if (user == null) return;

        var quiz = await _quizProvider.GetByIdAsync(resource.Id, new CancellationToken());
        if (quiz == null) return;

        if (resource.CreatorId == user.Id
            || resource.Access == QuizAccess.Public
            || resource.AccessCode == quiz.AccessCode
           )

        {
            context.Succeed(requirement);
        }
    }
}