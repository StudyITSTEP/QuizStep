using QuizStep.Core.Entities;

namespace QuizStep.Core.Interfaces;

public interface ICategory
{
    Task<Category?> GetByIdAsync(int id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> CreateAsync(Category category);
    Task<Category?> UpdateAsync(Category category);
    Task<bool> DeleteAsync(int id);
}