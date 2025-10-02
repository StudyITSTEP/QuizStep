using MediatR;
using Microsoft.AspNetCore.Identity;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Application.DTOs.User;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.User
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserWithRolesDto>>>
    {
        private readonly UserManager<Core.Entities.User> _userManager;

        public GetAllUsersQueryHandler(UserManager<Core.Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<List<UserWithRolesDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.ToList();
            var result = new List<UserWithRolesDto>();

            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                result.Add(new UserWithRolesDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Roles = roles
                });
            }

            return result;
        }
    }

}
