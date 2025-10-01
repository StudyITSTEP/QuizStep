using MediatR;
using QuizStep.Application.DTOs.Quiz;

namespace QuizStep.Application.Commands___Queries.Quiz;

public record GetQuizzesQuery: IRequest<IEnumerable<QuizDto>>;
