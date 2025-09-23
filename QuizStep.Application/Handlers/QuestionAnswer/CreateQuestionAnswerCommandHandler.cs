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
    public class CreateQuestionAnswerCommandHandler : IRequestHandler<CreateQuestionAnswerCommand, Result<QuizStep.Core.Entities.QuestionAnswer>>
    {
        private readonly IQuestionAnswer _repo;

        public CreateQuestionAnswerCommandHandler(IQuestionAnswer repo)
        {
            _repo = repo;
        }

        public async Task<Result<QuizStep.Core.Entities.QuestionAnswer>> Handle(CreateQuestionAnswerCommand request, CancellationToken cancellationToken)
        {
            var entity = new QuizStep.Core.Entities.QuestionAnswer
            {
                QuestionId = request.QuestionId,
                AnswerId = request.AnswerId,
                IsCorrect = request.IsCorrect
            };

            await _repo.AddAsync(entity, cancellationToken);
            return Result<QuizStep.Core.Entities.QuestionAnswer>.Success(entity);
        }
    }
}
