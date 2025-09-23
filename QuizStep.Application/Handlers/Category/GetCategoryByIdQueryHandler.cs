using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Category;
using QuizStep.Application.DTOs;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Category;

public class GetCategoryByIdQueryHandler: IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
{
    private readonly ICategory _category;
    private readonly IMapper _mapper;
    
    public GetCategoryByIdQueryHandler(ICategory category, IMapper mapper)
    {
        _category = category;
        _mapper = mapper;
    }
    
    public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _category.GetByIdAsync(request.CategoryId);
        return _mapper.Map<CategoryDto>(category);
    }
}