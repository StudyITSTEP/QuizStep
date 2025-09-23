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
    public class GetAnswerByIdQueryHandler : IRequestHandler<GetAnswerByIdQuery, Result<QuizStep.Core.Entities.Answer>>
    {
        private readonly IAnswer _answerRepo;

        public GetAnswerByIdQueryHandler(IAnswer answerRepo)
        {
            _answerRepo = answerRepo;
        }

        public async Task<Result<QuizStep.Core.Entities.Answer>> Handle(GetAnswerByIdQuery request, CancellationToken cancellationToken)
        {
            var answer = await _answerRepo.GetByIdAsync(request.Id, cancellationToken);

            if (answer == null)
                return QueryError.EntityNotExist;

            return Result<QuizStep.Core.Entities.Answer>.Success(answer);
        }
    }
}
