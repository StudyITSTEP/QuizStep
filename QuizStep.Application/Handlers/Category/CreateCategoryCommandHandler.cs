using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Category;
using QuizStep.Application.DTOs;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Category;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly ICategory _category;

    public CreateCategoryCommandHandler(IMapper mapper, ICategory category)
    {
        _mapper = mapper;
        _category = category;
    }

    public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var cat = _mapper.Map<Core.Entities.Category>(request);
        var newCategory = await _category.CreateAsync(cat);
        return _mapper.Map<CategoryDto>(newCategory);
    }
}