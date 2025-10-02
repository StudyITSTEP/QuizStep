using MediatR;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Commands___Queries.QuestionAnswer
{
    public record UpdateQuestionAnswerCommand(int Id, int QuestionId, int AnswerId, bool IsCorrect) : IRequest<Result<QuizStep.Core.Entities.QuestionAnswer>>;
}
