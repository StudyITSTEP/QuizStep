using MediatR;
using QuizStep.Application.Commands___Queries.Answer;
using QuizStep.Application.Handlers.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Answer
{
    public class GetAnswersQueryHandler : IRequestHandler<GetAnswersQuery, Result<IEnumerable<QuizStep.Core.Entities.Answer>>>
    {
        private readonly IAnswer _answerRepo;

        public GetAnswersQueryHandler(IAnswer answerRepo)
        {
            _answerRepo = answerRepo;
        }

        public async Task<Result<IEnumerable<QuizStep.Core.Entities.Answer>>> Handle(GetAnswersQuery request, CancellationToken cancellationToken)
        {
            var answers = await _answerRepo.GetAllAsync(cancellationToken);
            return Result<IEnumerable<QuizStep.Core.Entities.Answer>>.Success(answers);
        }
    }
}
