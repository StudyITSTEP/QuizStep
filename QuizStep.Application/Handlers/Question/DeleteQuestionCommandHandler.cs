using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Question;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Question;

public class DeleteQuestionCommandHandler: IRequestHandler<DeleteQuestionCommand, Result>
{
    private readonly IQuizProvider _quizProvider;

    public DeleteQuestionCommandHandler(IQuizProvider quizProvider)
    {
        _quizProvider = quizProvider;
    }
    
    public async Task<Result> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
       var result = await _quizProvider.RemoveQuestionAsync(request.QuestionId, cancellationToken);
       return result;
    }
}