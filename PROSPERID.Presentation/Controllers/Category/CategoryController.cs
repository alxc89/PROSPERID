using Microsoft.AspNetCore.Mvc;
using PROSPERID.Application.DTOs.Category;
using PROSPERID.Application.ModelViews.Category;
using PROSPERID.Application.ModelViews.Transaction;
using PROSPERID.Application.Services.Category;
using PROSPERID.Application.Services.Shared;
using System.Net.Mime;

namespace PROSPERID.Presentation.Controllers.Category;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    /// <summary>
    /// Retorna uma Categoria.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<CategoryView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(long id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category.Data == null)
            return BadRequest(category?.Message);
        return Ok(category);
    }

    /// <summary>
    /// Retorna uma lista de Categorias.
    /// </summary>
    [HttpGet()]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<CategoryView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var category = await _categoryService.GetCategoriesAsync();
        if (category.Data == null && !category.IsSuccess)
            return BadRequest(category?.Message);
        return Ok(category);
    }

    /// <summary>
    /// Criação de uma Categoria.
    /// </summary>
    /// <param name="createCategoryDTO"></param>
    /// <returns></returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<CategoryView>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(CreateCategoryDTO createCategoryDTO)
    {
        var newCategory = await _categoryService.CreateCategoryAsync(createCategoryDTO);
        if (!newCategory.IsSuccess)
            return BadRequest(newCategory.Message);
        return CreatedAtAction("Get", new { id = newCategory.Data?.Id }, newCategory.Data);
    }

    /// <summary>
    /// Alteração de uma Categoria.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateCategoryDTO"></param>
    /// <returns></returns>
    [HttpPut()]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<CategoryView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(long id, UpdateCategoryDTO updateCategoryDTO)
    {
        var updateCategory = await _categoryService.UpdateCategoryAsync(id, updateCategoryDTO);
        if (!updateCategory.IsSuccess)
            return BadRequest(updateCategory?.Message);
        return Ok(updateCategory);
    }

    /// <summary>
    /// Deletar uma Categoria.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<CategoryView>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(long id)
    {
        var deleteCategory = await _categoryService.DeleteCategoryAsync(id);
        if (!deleteCategory.IsSuccess)
            return BadRequest(deleteCategory?.Message);
        return Ok(deleteCategory.Message);
    }
}
