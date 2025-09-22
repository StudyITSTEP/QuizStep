using MediatR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.Question;

public record GetQuestionByIdQuery(int QuestionId): IRequest<Result<QuestionDto>>;
