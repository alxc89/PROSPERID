using PROSPERID.Core.Entities;

namespace PROSPERID.Core.Interface.Repositories;

public interface ICategoryRepository
{
    Task<Category> CreateCategoryAsync(Category category);
    Task<IEnumerable<Category>> GetCategoryAsync();
    Task<Category?> GetCategoryByIdAsync(long id);
    Task<bool> AnyCategoryAsync(string name);
    Task<Category?> UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(long id);
}
