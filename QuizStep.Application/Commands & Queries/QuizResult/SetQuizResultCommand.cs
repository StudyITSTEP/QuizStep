using MediatR;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.QuizResult;

public record SetQuizResultCommand(string UserId, int QuizId, decimal Score) : IRequest<Result>;