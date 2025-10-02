using MediatR;
using QuizStep.Application.DTOs.User;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Commands___Queries.User
{
    public record GetAllUsersQuery() : IRequest<Result<List<UserWithRolesDto>>>;
}
