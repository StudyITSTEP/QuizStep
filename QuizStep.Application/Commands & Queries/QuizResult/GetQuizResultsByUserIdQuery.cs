using MediatR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.QuizResult;

public record GetQuizResultsByUserIdQuery(string UserId) : IRequest<Result<IEnumerable<QuizResultDto>>>;