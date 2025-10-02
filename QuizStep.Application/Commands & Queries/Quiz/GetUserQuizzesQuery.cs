using MediatR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.Quiz;

public record GetUserQuizzesQuery(string userId) : IRequest<IEnumerable<QuizDetailsDto>>;