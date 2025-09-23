using MediatR;
using QuizStep.Application.DTOs;

namespace QuizStep.Application.Commands___Queries.Category;

public class GetCategoriesQuery: IRequest<IEnumerable<CategoryDto>>;