using Microsoft.AspNetCore.Mvc;
using PROSPERID.Application.Services.Category;
using PROSPERID.Application.Services.DTOs.Category;

namespace PROSPERID.Presentation.Controllers.Category;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCategoryDTO createCategoryDTO)
    {
        var newCategory = await _categoryService.CreateCategoryAsync(createCategoryDTO);
        if (!newCategory.IsSuccess)
            return BadRequest(newCategory.Message);
        return Ok(newCategory);
    }
}
