using PROSPERID.Domain.Entities;

namespace PROSPERID.Domain.Interface.Repositories;

public interface ICategoryRepository
{
    Task<Category> CreateCategoryAsync(Category category);
    Task<IEnumerable<Category>> GetCategoryAsync();
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<bool> AnyCategoryAsync(string name);
    Task<Category?> UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Guid id);
}
