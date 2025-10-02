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
        var aw = new Dictionary<int, int>();
        foreach (var i in request.AnswerQuestions)
        {
            aw[i.QuestionId] = i.AnswerId;
        }

        var score = await _quizProvider.ResolveScoreAsync(request.QuizId, aw, cancellationToken);

        await _quizProvider.SetQuizResultAsync(
            new Core.Entities.QuizResult() { QuizId = request.QuizId, UserId = request.UserId, Score = score },
            cancellationToken);
        return Result.Success();
    }
}