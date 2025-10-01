using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Errors.General;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Quiz
{
    public class GetTestByIdQueryHandler : IRequestHandler<GetByIdQuizQuery, Result<QuizDto>>
    {
        private readonly IQuizProvider _quizProviderRepo;
        private readonly IMapper _mapper;

        public GetTestByIdQueryHandler(IQuizProvider quizProviderRepo, IMapper mapper)
        {
            _quizProviderRepo = quizProviderRepo;
            _mapper = mapper;
        }

        public async Task<Result<QuizDto>> Handle(GetByIdQuizQuery request, CancellationToken cancellationToken)
        {
            var test = await _quizProviderRepo.GetByIdAsync(request.Id, cancellationToken);
            if (test == null)
                return QueryError.EntityNotExist;
            
            return _mapper.Map<QuizDto>(test);
        }
    }

}
