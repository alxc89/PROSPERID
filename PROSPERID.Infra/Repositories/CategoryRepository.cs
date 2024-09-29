using Microsoft.EntityFrameworkCore;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Interface.Repositories;
using PROSPERID.Infra.Context;

namespace PROSPERID.Infra.Repositories;

public class CategoryRepository(DataContext context) : ICategoryRepository
{
    private readonly DataContext _context = context;

    public async Task<Category?> GetCategoryByIdAsync(long id)
    {
        var category = _context.Categories;
        var result = await category.SingleOrDefaultAsync(c => c.Id == id);
        return result;
    }

    public async Task<IEnumerable<Category>> GetCategoryAsync()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        try
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<Category?> UpdateCategoryAsync(Category category)
    {
        var categoryUpdated = await _context.Categories.SingleOrDefaultAsync(c => c.Id == category.Id);
        if (categoryUpdated == null)
            return null;
        try
        {
            _context.Entry(categoryUpdated).CurrentValues.SetValues(category);
            await _context.SaveChangesAsync();
            return categoryUpdated;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task DeleteCategoryAsync(long id)
    {
        var categoryDeleted = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
        if (categoryDeleted == null) return;
        
        try
        {
            _context.Categories.Remove(categoryDeleted);
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<bool> AnyCategoryAsync(string name)
    {
        return await _context.Categories.AnyAsync(c => c.Name.Equals(name));
    }
}
