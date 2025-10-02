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
    public class DeleteQuestionAnswerCommandHandler : IRequestHandler<DeleteQuestionAnswerCommand, Result>
    {
        private readonly IQuestionAnswer _repo;

        public DeleteQuestionAnswerCommandHandler(IQuestionAnswer repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(DeleteQuestionAnswerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                return QueryError.EntityNotExist;

            await _repo.DeleteAsync(request.Id, cancellationToken);
            return Result.Success();
        }
    }
}
