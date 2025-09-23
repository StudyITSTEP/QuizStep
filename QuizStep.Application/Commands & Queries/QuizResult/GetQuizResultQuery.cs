using MediatR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.QuizResult;

public record GetQuizResultQuery(string UserId, int QuizId): IRequest<Result<QuizResultDto>>;
