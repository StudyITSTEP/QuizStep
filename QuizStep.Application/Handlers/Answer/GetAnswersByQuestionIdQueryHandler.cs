using MediatR;
using QuizStep.Application.Commands___Queries.Answer;
using QuizStep.Core.Errors.General;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Answer
{
    public class GetAnswersByQuestionIdQueryHandler
    : IRequestHandler<GetAnswersByQuestionIdQuery, Result<IEnumerable<Core.Entities.Answer>>>
    {
        private readonly IAnswer _answerRepo;

        public GetAnswersByQuestionIdQueryHandler(IAnswer answerRepo)
        {
            _answerRepo = answerRepo;
        }

        public async Task<Result<IEnumerable<Core.Entities.Answer>>> Handle(GetAnswersByQuestionIdQuery request, CancellationToken cancellationToken)
        {
            var answers = await _answerRepo.GetByQuestionIdAsync(request.QuestionId, cancellationToken);

            if (!answers.Any())
                return QueryError.EntityNotExist;

            return Result<IEnumerable<Core.Entities.Answer>>.Success(answers);
        }
    }
}
