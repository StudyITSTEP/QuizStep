using MediatR;
using QuizStep.Application.DTOs.Quiz;

namespace QuizStep.Application.Commands___Queries.QuizResult;

public record GetQuizResultsByQuizIdQuery(int QuizId) : IRequest<IEnumerable<QuizResultDto>>;