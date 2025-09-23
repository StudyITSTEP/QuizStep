using MediatR;
using QuizStep.Application.DTOs;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.Category;

public record CreateCategoryCommand(string Name): IRequest<Result<CategoryDto>>;