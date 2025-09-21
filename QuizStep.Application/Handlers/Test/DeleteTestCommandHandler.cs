using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizStep.Application.Commands___Queries.Test;
using QuizStep.Application.Interfaces;
using QuizStep.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Test
{
    public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand, Unit>
    {
        private readonly ITest _testRepo;

        public DeleteTestCommandHandler(ITest testRepo)
        {
            _testRepo = testRepo;
        }

        public async Task<Unit> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepo.GetByIdAsync(request.Id, cancellationToken);

            if (test == null)
                throw new KeyNotFoundException($"Test with Id {request.Id} not found");
            await _testRepo.DeleteAsync(test, cancellationToken);
            return Unit.Value;
        }
    }

}
