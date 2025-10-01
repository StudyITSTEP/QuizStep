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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly UserManager<Core.Entities.User> _userManager;

        public DeleteUserCommandHandler(UserManager<Core.Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return null;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded ? Result.Success() : Error.Failed;
        }
    }
}
