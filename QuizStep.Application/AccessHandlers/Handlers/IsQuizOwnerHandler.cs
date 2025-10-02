using Microsoft.AspNetCore.Authorization;
using QuizStep.Application.AccessHandlers.Requrements;
using QuizStep.Core.Interfaces;
using System.Security.Claims;

namespace QuizStep.Application.AccessHandlers.Handlers
{
    public class IsQuizOwnerHandler : AuthorizationHandler<IsQuizOwnerRequirement, QuizStep.Core.Entities.Quiz> 
    {
        private readonly IUser _user;
        private readonly IQuizProvider _quizProvider;

        public IsQuizOwnerHandler(IUser user,  IQuizProvider quizProvider)
        {
            _user = user;
            _quizProvider = quizProvider;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, IsQuizOwnerRequirement requirement, QuizStep.Core.Entities.Quiz resource)
        {
            var user = await _user.GetUserAsync();
            var quiz = await _quizProvider.GetByIdAsync(resource.Id, new CancellationToken());
            if (user == null)
            {
                return;
            }

            if(quiz.Creator.Id == user.Id)
            {
                context.Succeed(requirement);
            }
        }
    }
}
