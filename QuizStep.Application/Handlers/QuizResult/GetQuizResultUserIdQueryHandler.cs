using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.QuizResult;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.QuizResult;

public class
    GetQuizResultUserIdQueryHandler : IRequestHandler<GetQuizResultsByUserIdQuery, Result<IEnumerable<QuizResultDto>>>
{
    private readonly IQuizResultProvider _quizResultProvider;
    private readonly IMapper _mapper;

    public GetQuizResultUserIdQueryHandler(IQuizResultProvider quizResultProvider, IMapper mapper)
    {
        _quizResultProvider = quizResultProvider;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<QuizResultDto>>> Handle(GetQuizResultsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var results = await _quizResultProvider.GetQuizResultsByUserIdAsync(request.UserId);
        var dto =  _mapper.Map<List<QuizResultDto>>(results.Value);
        return dto;
    }
}