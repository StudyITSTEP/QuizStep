using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using QuizStep.Application.Commands___Queries.Test;
using QuizStep.Application.DTOs.Test;
using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.Handlers.Test
{
    internal class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, Result<TestDto>>
    {
        private readonly ITest _testRepo;
        private readonly IMapper _mapper;

        public CreateTestCommandHandler(ITest testRepo, IMapper mapper)
        {
            _testRepo = testRepo;
            _mapper = mapper;
        }

        public async Task<Result<TestDto>> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var test = _mapper.Map<QuizStep.Core.Entities.Test>(request.Test);
            await _testRepo.AddAsync(test, cancellationToken);

            return Result<TestDto>.Ok(_mapper.Map<TestDto>(test));
        }
    }
}
