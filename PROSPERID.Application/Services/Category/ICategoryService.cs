using PROSPERID.Application.DTOs.Category;
using PROSPERID.Application.ModelViews.Category;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.Category;

public interface ICategoryService
{
    Task<ServiceResponse<CategoryView>> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
    Task<ServiceResponse<CategoryView>> UpdateCategoryAsync(long id, UpdateCategoryDTO updateCategoryDTO);
    Task<ServiceResponse<CategoryView>> DeleteCategoryAsync(long id);
    Task<ServiceResponse<CategoryView>> GetCategoryByIdAsync(long id);
    Task<ServiceResponse<IEnumerable<CategoryView>>> GetCategoriesAsync();
}
