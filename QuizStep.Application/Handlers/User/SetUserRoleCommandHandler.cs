using MediatR;
using Microsoft.AspNetCore.Identity;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.User
{
    public class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand, Result>
    {
        private readonly UserManager<Core.Entities.User> _userManager;

        public SetUserRoleCommandHandler(UserManager<Core.Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return null;

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

            var result = await _userManager.AddToRoleAsync(user, request.Role);

            return result.Succeeded ? Result.Success() : Error.Failed;
        }
    }

}
