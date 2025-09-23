using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Category;
using QuizStep.Application.DTOs;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Category;

public class UpdateCategoryCommandHandler: IRequestHandler<UpdateCategoryCommand, Result<CategoryDto>>
{
    private readonly ICategory _category;
    private readonly IMapper _mapper;
    
    public UpdateCategoryCommandHandler(ICategory category, IMapper mapper)
    {
        _category = category;
        _mapper = mapper;
    }
    
    public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
       var category = _mapper.Map<Core.Entities.Category>(request);
       var newCategory = await _category.UpdateAsync(category);
       return _mapper.Map<CategoryDto>(newCategory);
    }
}