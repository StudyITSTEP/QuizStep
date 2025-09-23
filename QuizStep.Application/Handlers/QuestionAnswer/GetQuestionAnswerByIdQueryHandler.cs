using MediatR;
using QuizStep.Application.Commands___Queries.QuestionAnswer;
using QuizStep.Core.Errors.General;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.QuestionAnswer
{
    public class GetQuestionAnswerByIdQueryHandler : IRequestHandler<GetQuestionAnswerByIdQuery, Result<Core.Entities.QuestionAnswer>>
    {
        private readonly IQuestionAnswer _repo;

        public GetQuestionAnswerByIdQueryHandler(IQuestionAnswer repo)
        {
            _repo = repo;
        }

        public async Task<Result<Core.Entities.QuestionAnswer>> Handle(GetQuestionAnswerByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                return QueryError.EntityNotExist;

            return Result<Core.Entities.QuestionAnswer>.Success(entity);
        }
    }
}
