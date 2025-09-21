using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using QuizStep.Application.AccessHandlers.Requrements;
using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Test
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
