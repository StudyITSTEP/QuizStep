using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Question;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.Handlers.Question;

public class GetQuestionsByQuizQueryHandler : IRequestHandler<GetQuestionsByQuizQuery, IEnumerable<QuestionDto>>
{
    private readonly IMapper _mapper;
    private readonly IQuizProvider _quizProvider;

    public GetQuestionsByQuizQueryHandler(IMapper mapper, IQuizProvider quizProvider)
    {
        _mapper = mapper;
        _quizProvider = quizProvider;
    }

    public async Task<IEnumerable<QuestionDto>> Handle(GetQuestionsByQuizQuery request, CancellationToken cancellationToken)
    {
        var questions = await _quizProvider.GetQuestionsByQuizAsync(request.QuizId, cancellationToken);
        return _mapper.Map<IEnumerable<QuestionDto>>(questions);
    }
}