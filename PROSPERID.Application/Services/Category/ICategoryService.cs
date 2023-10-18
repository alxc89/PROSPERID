using PROSPERID.Application.DTOs.Category;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.Category;

public interface ICategoryService
{
    Task<ServiceResponse<CategoryDTO>> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
    Task<ServiceResponse<UpdateCategoryDTO>> UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO);
    Task<ServiceResponse<CategoryDTO>> DeleteCategoryAsync(Guid id);
    Task<ServiceResponse<CategoryDTO>> GetCategoryByIdAsync(Guid id);
}
