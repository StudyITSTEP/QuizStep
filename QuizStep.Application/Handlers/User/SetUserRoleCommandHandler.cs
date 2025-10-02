using MediatR;
using Microsoft.AspNetCore.Identity;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizStep.Core.Errors.General;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.Handlers.User
{
    public class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand, Result>
    {
        private readonly IUser _userManager;

        public SetUserRoleCommandHandler(IUser userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserByIdAsync(request.UserId);
            if (user == null) return QueryError.EntityNotExist;

            var result = await _userManager.AddToRolesAsync(user, request.Roles);

            return result.Succeeded ? Result.Success() : Error.Failed;
        }
    }
}