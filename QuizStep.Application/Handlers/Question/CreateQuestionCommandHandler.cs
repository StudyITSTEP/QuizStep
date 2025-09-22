using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Question;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Question;

public class CreateQuestionCommandHandler: IRequestHandler<CreateQuestionCommand, Result<QuestionDto>>
{
    private readonly IMapper _mapper;
    private readonly IQuizProvider _quizProvider;

    public CreateQuestionCommandHandler(IMapper mapper, IQuizProvider quizProvider)
    {
        _mapper = mapper;
        _quizProvider = quizProvider;
    }
    
    public async Task<Result<QuestionDto>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = _mapper.Map<Core.Entities.Question>(request);
        var createdQuestion = await _quizProvider.CreateQuestionAsync(question, cancellationToken); 
        return _mapper.Map<QuestionDto>(createdQuestion);
    }
}