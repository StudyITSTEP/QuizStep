using MediatR;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Commands___Queries.Answer
{
    public record GetAnswersQuery() : IRequest<Result<IEnumerable<QuizStep.Core.Entities.Answer>>>;
}
