using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Test;
using QuizStep.Application.DTOs.Test;
using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.Handlers.Test
{
    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, Result<TestDto>>
    {
        private readonly ITest _testRepo;
        private readonly IMapper _mapper;

        public UpdateTestCommandHandler(ITest testRepo, IMapper mapper)
        {
            _testRepo = testRepo;
            _mapper = mapper;
        }

        public async Task<Result<TestDto>> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testRepo.GetByIdAsync(request.Test.Id, cancellationToken);

            if (test == null)
                return Result<TestDto>.Fail($"Test with Id {request.Test.Id} not found");
            await _testRepo.UpdateAsync(test, cancellationToken);

            return Result<TestDto>.Ok(_mapper.Map<TestDto>(test));
        }
    }
}
