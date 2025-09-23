using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Category;
using QuizStep.Core.Errors.General;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Category;

public class DeleteCategoryCommandHandler: IRequestHandler<DeleteCategoryCommand, Result>
{
    private readonly ICategory _category;

    public DeleteCategoryCommandHandler(ICategory category)
    {
        _category = category;
    }
    
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _category.DeleteAsync(request.CategoryId);
        return result ? Result.Success() : QueryError.EntityNotExist;
    }
}