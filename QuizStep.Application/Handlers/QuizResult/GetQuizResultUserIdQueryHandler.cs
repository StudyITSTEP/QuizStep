using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.QuizResult;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.QuizResult;

public class GetQuizResultUserIdQueryHandler: IRequestHandler<GetQuizResultByUserIdQuery, Result<QuizResultDto>>
{
    private readonly IQuizProvider _quizProvider;
    private readonly IMapper _mapper;

    public GetQuizResultUserIdQueryHandler(IQuizProvider quizProvider, IMapper mapper)
    {
        _quizProvider = quizProvider;
        _mapper = mapper;
    }
    
    public Task<Result<QuizResultDto>> Handle(GetQuizResultByUserIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}