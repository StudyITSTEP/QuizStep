using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.QuizResult;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.QuizResult;

public class SetQuizResultCommandHandler : IRequestHandler<SetQuizResultCommand, Result>
{
    private readonly IQuizProvider _quizProvider;
    private readonly IMapper _mapper;

    public SetQuizResultCommandHandler(IQuizProvider quizProvider, IMapper mapper)
    {
        _quizProvider = quizProvider;
        _mapper = mapper;
    }

    public async Task<Result> Handle(SetQuizResultCommand request, CancellationToken cancellationToken)
    {
        var quizResult = _mapper.Map<Core.Entities.QuizResult>(request);
        await _quizProvider.SetQuizResultAsync(quizResult, cancellationToken);
        return Result.Success();
    }
}