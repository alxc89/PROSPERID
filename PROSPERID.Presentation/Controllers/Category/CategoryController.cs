using Microsoft.AspNetCore.Mvc;
using PROSPERID.Application.DTOs.Category;
using PROSPERID.Application.Services.Category;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category.Data == null)
            return BadRequest(category?.Message);
        return Ok(category);
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        var category = await _categoryService.GetCategoriesAsync();
        if (category.Data == null && !category.IsSuccess)
            return BadRequest(category?.Message);
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCategoryDTO createCategoryDTO)
    {
        var newCategory = await _categoryService.CreateCategoryAsync(createCategoryDTO);
        if (!newCategory.IsSuccess)
            return BadRequest(newCategory.Message);
        return CreatedAtAction("Get", new { id = newCategory.Data?.Id }, newCategory.Data);
    }

    [HttpPut()]
    public async Task<IActionResult> Put(UpdateCategoryDTO updateCategoryDTO)
    {
        var updateCategory = await _categoryService.UpdateCategoryAsync(updateCategoryDTO);
        if (!updateCategory.IsSuccess)
            return BadRequest(updateCategory?.Message);
        return Ok(updateCategory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteCategory = await _categoryService.DeleteCategoryAsync(id);
        if (!deleteCategory.IsSuccess)
            return BadRequest(deleteCategory?.Message);
        return Ok(deleteCategory.Message);
    }
}
