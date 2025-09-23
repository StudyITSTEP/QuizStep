using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Category;
using QuizStep.Application.DTOs;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.Handlers.Category;

public class GetCategoriesQueryHandler: IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly ICategory _category;

    public GetCategoriesQueryHandler(IMapper mapper, ICategory category)
    {
        _mapper = mapper;
        _category = category;
    }
    
    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _category.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }
}