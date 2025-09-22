using Microsoft.AspNetCore.Authorization;
using QuizStep.Application.AccessHandlers.Requrements;
using QuizStep.Core.Interfaces;
using System.Security.Claims;

namespace QuizStep.Application.AccessHandlers.Handlers
{
    public class IsQuizOwnerHandler : AuthorizationHandler<IsQuizOwnerRequirement, QuizStep.Core.Entities.Quiz> 
    {
        private readonly IUser _user;

        public IsQuizOwnerHandler(IUser user)
        {
            _user = user;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, IsQuizOwnerRequirement requirement, QuizStep.Core.Entities.Quiz resource)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return;
            }

            var user = await _user.GetUserByIdAsync(userId);
            if (user == null)
            {
                return;
            }

            if(resource.Creator.Id == userId)
            {
                context.Succeed(requirement);
            }
        }
    }
}
