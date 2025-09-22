using Microsoft.AspNetCore.Authorization;
using QuizStep.Application.AccessHandlers.Requirements;
using QuizStep.Core.Entities;
using QuizStep.Core.Enums;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.AccessHandlers.Handlers;

public class QuizAccessHandler : AuthorizationHandler<QuizAccessRequirement, Quiz>
{
    private IUser _user;

    public QuizAccessHandler(IUser user)
    {
        _user = user;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        QuizAccessRequirement requirement, Quiz resource)
    {
        var user = await _user.GetUserAsync();

        if (user == null) return;

        if (resource.CreatorId == user.Id
            || resource.Access == QuizAccess.Public
            
           )
        {
            context.Succeed(requirement);
        }
    }
}