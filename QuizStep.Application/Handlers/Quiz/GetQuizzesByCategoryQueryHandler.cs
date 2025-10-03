using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Errors.General;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Quiz
{
    public class GetQuizzesByCategoryQueryHandler
    : IRequestHandler<GetQuizzesByCategoryQuery, Result<List<QuizDto>>>
    {
        private readonly IQuizProvider _quizProviderRepo;
        private readonly IMapper _mapper;

        public GetQuizzesByCategoryQueryHandler(IQuizProvider quizProviderRepo, IMapper mapper)
        {
            _quizProviderRepo = quizProviderRepo;
            _mapper = mapper;
        }

        public async Task<Result<List<QuizDto>>> Handle(GetQuizzesByCategoryQuery request, CancellationToken cancellationToken)
        {
            var quizzes = await _quizProviderRepo.GetByCategoryAsync(request.CategoryId, cancellationToken);
            if (quizzes == null || !quizzes.Any())
                return QueryError.EntityNotExist;

            return _mapper.Map<List<QuizDto>>(quizzes);
        }
    }
}
