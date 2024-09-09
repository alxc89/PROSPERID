using PROSPERID.Application.DTOs.Category;
using PROSPERID.Application.ModelViews.Category;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Core.Interface.Repositories;

namespace PROSPERID.Application.Services.Category;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
    private readonly ICategoryRepository _repository = repository;

    public async Task<ServiceResponse<CategoryView>> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
    {
        var validate = ValidateCategoryInput<CreateCategoryDTO>.Validate(createCategoryDTO.Name);
        if (validate != null)
            return ServiceResponseHelper.Error<CategoryView>(validate.Status, validate.Message);
        if (await _repository.AnyCategoryAsync(createCategoryDTO.Name))
            return ServiceResponseHelper.Error<CategoryView>(400, "Requisição inválida, Categoria já existente");
        var category = new Core.Entities.Category(createCategoryDTO.Name);

        try
        {
            CategoryView createCategory = await _repository.CreateCategoryAsync(category);
            return ServiceResponseHelper.Success(200, "Categoria criada com sucesso!", createCategory);
        }
        catch
        {
            return ServiceResponseHelper.Error<CategoryView>(500, "Erro Interno!");
        }
    }

    public async Task<ServiceResponse<CategoryView>> UpdateCategoryAsync(long id, UpdateCategoryDTO updateCategoryDTO)
    {
        var validate = ValidateCategoryInput<UpdateCategoryDTO>.Validate(updateCategoryDTO.Name);
        if (validate != null)
            return ServiceResponseHelper.Error<CategoryView>(validate.Status, validate.Message);

        Core.Entities.Category? category = await _repository.GetCategoryByIdAsync(id);
        if (category is null)
            return ServiceResponseHelper.Error<CategoryView>(404, "Requisição inválida, Categoria não encontrada");

        category.Update(updateCategoryDTO.Name);

        try
        {
            var categoryView = await _repository.UpdateCategoryAsync(category);
            return ServiceResponseHelper.Success(200, "Categoria atualizada com sucesso!", (CategoryView)categoryView!);
        }
        catch
        {
            return ServiceResponseHelper.Error<CategoryView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<CategoryView>> DeleteCategoryAsync(long id)
    {
        var category = await _repository.GetCategoryByIdAsync(id);
        if (category is null)
            return ServiceResponseHelper.Error<CategoryView>(404, "Requisição inválida, Categoria não encontrada!");
        try
        {
            await _repository.DeleteCategoryAsync(id);
            return ServiceResponseHelper.Success<CategoryView>(200, "Categoria deletada!");
        }
        catch
        {
            return ServiceResponseHelper.Error<CategoryView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<CategoryView>> GetCategoryByIdAsync(long id)
    {
        var category = await _repository.GetCategoryByIdAsync(id);
        if (category is null)
            return ServiceResponseHelper.Error<CategoryView>(404, "Requisição inválida, Categoria não encontrada!");

        return ServiceResponseHelper.Success(200, "Retornado com sucesso", (CategoryView)category);
    }

    public async Task<ServiceResponse<IEnumerable<CategoryView>>> GetCategoriesAsync()
    {
        var categories = await _repository.GetCategoryAsync();
        List<CategoryView> categoryView = [];
        if (!categories.Any())
            return ServiceResponseHelper
                .Error<IEnumerable<CategoryView>>(404, "Requisição inválida, Nenhuma Categoria não encontrada!");
        foreach (var category in categories)
            categoryView.Add(category);
        return ServiceResponseHelper.Success(200, "Retornado com sucesso", (IEnumerable<CategoryView>)categoryView);
    }
}
