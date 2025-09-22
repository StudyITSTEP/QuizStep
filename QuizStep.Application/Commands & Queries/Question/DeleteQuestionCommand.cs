using MediatR;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.Question;

public record DeleteQuestionCommand(int QuestionId): IRequest<Result>;