using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Quiz
{
    internal class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, Result<QuizDto>>
    {
        private readonly IQuizProvider _quizProviderRepo;
        private readonly IMapper _mapper;

        public CreateQuizCommandHandler(IQuizProvider quizProviderRepo, IMapper mapper)
        {
            _quizProviderRepo = quizProviderRepo;
            _mapper = mapper;
        }

        public async Task<Result<QuizDto>> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            var test = _mapper.Map<QuizStep.Core.Entities.Quiz>(request.Test);
            await _quizProviderRepo.AddAsync(test, cancellationToken);
            var dto = _mapper.Map<QuizDto>(test);
            return dto;
        }
    }
}
