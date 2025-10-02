using MediatR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.QuizResult;

public class SetQuizResultCommand() : IRequest<Result>
{
    public string UserId  { get; set; } = null!;
    public int QuizId  { get; set; }
    public IEnumerable<AnswerQuestionDto>  AnswerQuestions { get; set; } = null!;
}