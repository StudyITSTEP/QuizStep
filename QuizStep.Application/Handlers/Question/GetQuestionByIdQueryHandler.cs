using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Question;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Question;

public class GetQuestionByIdQueryHandler: IRequestHandler<GetQuestionByIdQuery, Result<QuestionDto>>
{
    private readonly IMapper _mapper;
    private readonly IQuizProvider _quizProvider;

    public GetQuestionByIdQueryHandler(IMapper mapper, IQuizProvider quizProvider)
    {
        _mapper = mapper;
        _quizProvider = quizProvider;
    }
    
    public async Task<Result<QuestionDto>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        var question = await _quizProvider.GetQuestionByIdAsync(request.QuestionId, cancellationToken);
        return _mapper.Map<QuestionDto>(question);
    }
}