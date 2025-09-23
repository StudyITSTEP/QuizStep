using MediatR;
using QuizStep.Application.DTOs;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.Category;

public record GetCategoryByIdQuery(int CategoryId): IRequest<Result<CategoryDto>>;
