using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.QuizResult;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.QuizResult;

public class GetQuizResultQueryHandler : IRequestHandler<GetQuizResultQuery, Result<QuizResultDto>>
{
    private readonly IQuizProvider _quizProvider;
    private readonly IMapper _mapper;

    public GetQuizResultQueryHandler(IQuizProvider quizProvider, IMapper mapper)
    {
        _quizProvider = quizProvider;
        _mapper = mapper;
    }

    public async Task<Result<QuizResultDto>> Handle(GetQuizResultQuery request, CancellationToken cancellationToken)
    {
        var result = await _quizProvider.GetQuizResultAsync(request.UserId, request.QuizId, cancellationToken);
        return _mapper.Map<QuizResultDto>(result);
    }
}