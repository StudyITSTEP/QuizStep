using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizStep.Application.Commands___Queries.Test;
using QuizStep.Application.Interfaces;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Test
{
    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, Unit>
    {
        private readonly ITest _testRepo;
        private readonly IMapper _mapper;

        public UpdateTestCommandHandler(ITest testRepo, IMapper mapper)
        {
            _testRepo = testRepo;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepo.GetByIdAsync(request.Test.Id, cancellationToken);

            if (test == null)
                throw new KeyNotFoundException($"Test with Id {request.Test.Id} not found");

            _mapper.Map(request.Test, test);
            await _testRepo.UpdateAsync(test, cancellationToken);

            return Unit.Value;
        }
    }
}
