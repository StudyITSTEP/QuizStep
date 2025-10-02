using MediatR;
using QuizStep.Core.Entities;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Commands___Queries.Answer
{
    public record GetAnswerByIdQuery(int Id) : IRequest<Result<QuizStep.Core.Entities.Answer>>;
}
