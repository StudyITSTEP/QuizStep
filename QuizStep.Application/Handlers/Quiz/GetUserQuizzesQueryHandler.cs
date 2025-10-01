using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.Application.DTOs;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Quiz;

public class GetUserQuizzesQueryHandler: IRequestHandler<GetUserQuizzesQuery, IEnumerable<QuizDto>>
{
    private readonly IMapper _mapper;
    private readonly IQuizProvider _quizProvider;

    public GetUserQuizzesQueryHandler(IQuizProvider quizProvider, IMapper mapper)
    {
        _quizProvider = quizProvider;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<QuizDto>> Handle(GetUserQuizzesQuery request, CancellationToken cancellationToken)
    {
        var result = await _quizProvider.GetByUserAsync(request.userId, cancellationToken);
        return _mapper.Map<IEnumerable<QuizDto>>(result);
    }
}