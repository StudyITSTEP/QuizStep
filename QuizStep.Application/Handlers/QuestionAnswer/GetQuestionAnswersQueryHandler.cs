using MediatR;
using QuizStep.Application.Commands___Queries.QuestionAnswer;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.QuestionAnswer
{
    public class GetQuestionAnswersQueryHandler : IRequestHandler<GetQuestionAnswersQuery, Result<IEnumerable<Core.Entities.QuestionAnswer>>>
    {
        private readonly IQuestionAnswer _repo;

        public GetQuestionAnswersQueryHandler(IQuestionAnswer repo)
        {
            _repo = repo;
        }

        public async Task<Result<IEnumerable<Core.Entities.QuestionAnswer>>> Handle(GetQuestionAnswersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repo.GetAllAsync(cancellationToken);
            return Result<IEnumerable<Core.Entities.QuestionAnswer>>.Success(entities);
        }
    }
}
