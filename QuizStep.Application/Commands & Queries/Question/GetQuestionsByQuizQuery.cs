using MediatR;
using QuizStep.Application.DTOs.Quiz;

namespace QuizStep.Application.Commands___Queries.Question;

public record GetQuestionsByQuizQuery(int QuizId) : IRequest<IEnumerable<QuestionDto>>;