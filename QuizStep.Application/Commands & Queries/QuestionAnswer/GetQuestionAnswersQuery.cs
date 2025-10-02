using MediatR;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Commands___Queries.QuestionAnswer
{
    public record GetQuestionAnswersQuery() : IRequest<Result<IEnumerable<Core.Entities.QuestionAnswer>>>;
}
