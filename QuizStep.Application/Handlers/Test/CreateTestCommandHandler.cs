using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Test;
using QuizStep.Application.Interfaces;
using QuizStep.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Test
{
    internal class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, int>
    {
        private readonly ITest _testRepo;
        private readonly IMapper _mapper;

        public CreateTestCommandHandler(ITest testRepo, IMapper mapper)
        {
            _testRepo = testRepo;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var test = _mapper.Map<QuizStep.Core.Entities.Test>(request.Test);
            await _testRepo.AddAsync(test, cancellationToken);

            return test.Id;
        }
    }
}
