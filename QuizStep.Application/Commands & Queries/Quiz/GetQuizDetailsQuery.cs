using MediatR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.Quiz;

public record GetQuizDetailsQuery(int QuizId) : IRequest<Result<QuizDetailsDto>>;
