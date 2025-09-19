using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using QuizStep.Application.Handlers.Test;
using QuizStep.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Test
{
    public class IsTestOwnerHandler : AuthorizationHandler<IsTestOwnerRequirement, QuizStep.Core.Entities.Test> 
    {
        private readonly UserManager<QuizStep.Core.Entities.User> _userManager; 

        public IsTestOwnerHandler(UserManager<Core.Entities.User> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, IsTestOwnerRequirement requirement, QuizStep.Core.Entities.Test resource)
        {
            var userId = _userManager.GetUserId(context.User);

            if (resource.Creator.Id == userId)
                context.Succeed(requirement);

            await Task.CompletedTask;
        }
    }
}
