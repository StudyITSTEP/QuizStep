using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Question;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Question;

public class UpdateQuestionCommandHandler: IRequestHandler<UpdateQuestionCommand, Result<QuestionDto>>
{
    private readonly IMapper _mapper;
    private readonly IQuizProvider _quizProvider;

    public UpdateQuestionCommandHandler(IMapper mapper, IQuizProvider quizProvider)
    {
        _mapper = mapper;
        _quizProvider = quizProvider;
    }
    
    public async Task<Result<QuestionDto>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = _mapper.Map<Core.Entities.Question>(request);
        var result = await _quizProvider.UpdateQuestionAsync(question, cancellationToken);
        return _mapper.Map<QuestionDto>(result);
    }
}