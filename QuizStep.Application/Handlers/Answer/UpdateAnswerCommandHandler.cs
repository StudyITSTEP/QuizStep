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
    public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand, Result>
    {
        private readonly IAnswer _answerRepo;

        public UpdateAnswerCommandHandler(IAnswer answerRepo)
        {
            _answerRepo = answerRepo;
        }

        public async Task<Result> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = await _answerRepo.GetByIdAsync(request.Id, cancellationToken);

            if (answer == null)
                return QueryError.EntityNotExist;

            answer.Text = request.Text;

            await _answerRepo.UpdateAsync(answer, cancellationToken);

            return Result.Success();
        }
    }
}
