using MediatR;
using QuizStep.Application.Commands___Queries.Answer;
using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Answer
{
    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, Result<int>>
    {
        private readonly IAnswer _answerRepo;

        public CreateAnswerCommandHandler(IAnswer answerRepo)
        {
            _answerRepo = answerRepo;
        }

        public async Task<Result<int>> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = new QuizStep.Core.Entities.Answer { Text = request.Text };
            await _answerRepo.AddAsync(answer, cancellationToken);

            return Result<int>.Success(answer.Id);
        }
    }
}
