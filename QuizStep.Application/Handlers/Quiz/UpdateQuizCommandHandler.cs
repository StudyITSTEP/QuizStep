using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Errors.General;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Quiz
{
    public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, Result<QuizDto>>
    {
        private readonly IQuizProvider _quizProviderRepo;
        private readonly IMapper _mapper;

        public UpdateQuizCommandHandler(IQuizProvider quizProviderRepo, IMapper mapper)
        {
            _quizProviderRepo = quizProviderRepo;
            _mapper = mapper;
        }

        public async Task<Result<QuizDto>> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            var test = await _quizProviderRepo.GetByIdAsync(request.Test.Id, cancellationToken);

            if (test == null)
                return QueryError.EntityNotExist;
            await _quizProviderRepo.UpdateAsync(test, cancellationToken);

            return _mapper.Map<QuizDto>(test);
        }
    }
}
