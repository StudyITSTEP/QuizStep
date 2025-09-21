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
    public class IsTestOwnerHandler : AuthorizationHandler<IsTestOwnerRequirement, QuizStep.Core.Entities.Test> 
    {
        private readonly IUser _user;

        public IsTestOwnerHandler(IUser user)
        {
            _user = user;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, IsTestOwnerRequirement requirement, QuizStep.Core.Entities.Test resource)
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
