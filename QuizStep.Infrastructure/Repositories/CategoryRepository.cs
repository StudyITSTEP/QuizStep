using Microsoft.EntityFrameworkCore;
using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;
using QuizStep.Infrastructure.Data;

namespace QuizStep.Infrastructure.Repositories;

public class CategoryRepository : ICategory
{
    private readonly ApplicationContext _context;

    public CategoryRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> CreateAsync(Category category)
    {
        var newCategory = _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return newCategory.Entity;
    }

    public async Task<Category?> UpdateAsync(Category category)
    {
        var newCategory = _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return newCategory.Entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}