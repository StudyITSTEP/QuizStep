using MediatR;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.Category;

public record DeleteCategoryCommand(int CategoryId): IRequest<Result>;