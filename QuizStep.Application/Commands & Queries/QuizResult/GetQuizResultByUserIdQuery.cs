using MediatR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.QuizResult;

public record GetQuizResultByUserIdQuery(string UserId) : IRequest<Result<QuizResultDto>>;