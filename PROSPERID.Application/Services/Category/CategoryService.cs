using PROSPERID.Application.DTOs.Category;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Core.Interface.Repositories;

namespace PROSPERID.Application.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<CategoryDTO>> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
    {
        var validate = ValidateCategoryInput<CreateCategoryDTO>.Validate(createCategoryDTO.Name);
        if (validate != null)
            return ServiceResponseHelper.Error<CategoryDTO>(validate.Status, validate.Message);
        if (await _repository.AnyCategoryAsync(createCategoryDTO.Name))
            return ServiceResponseHelper.Error<CategoryDTO>(400, "Requisição inválida, Categoria já existente");
        var category = new Core.Entities.Category(createCategoryDTO.Name);

        try
        {
            var createCategory = await _repository.CreateCategoryAsync(category);
            return ServiceResponseHelper
                .Success(200, "Categoria criada com sucesso!", new CategoryDTO(createCategory.Id, createCategory.Name));
        }
        catch
        {
            return ServiceResponseHelper.Error<CategoryDTO>(500, "Erro Interno!");
        }
    }

    public async Task<ServiceResponse<UpdateCategoryDTO>> UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
    {
        var validate = ValidateCategoryInput<UpdateCategoryDTO>.Validate(updateCategoryDTO.Name);
        if (validate != null)
            return ServiceResponseHelper.Error<UpdateCategoryDTO>(validate.Status, validate.Message);

        Core.Entities.Category category = await _repository.GetCategoryByIdAsync(updateCategoryDTO.Id);
        if (category is null)
            return ServiceResponseHelper.Error<UpdateCategoryDTO>(404, "Requisição inválida, Categoria não encontrada");

        category.Update(updateCategoryDTO.Name);

        try
        {
            await _repository.UpdateCategoryAsync(category);
            return ServiceResponseHelper.Success(200, "Categoria atualizada com sucesso!", new UpdateCategoryDTO(category.Id, category.Name));
        }
        catch
        {
            return ServiceResponseHelper.Error<UpdateCategoryDTO>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<CategoryDTO>> DeleteCategoryAsync(Guid id)
    {
        var category = await _repository.GetCategoryByIdAsync(id);
        if (category is null)
            return ServiceResponseHelper.Error<CategoryDTO>(404, "Requisição inválida, Categoria não encontrada!");
        try
        {
            await _repository.DeleteCategoryAsync(id);
            return ServiceResponseHelper.Success<CategoryDTO>(200, "Categoria deletada!");
        }
        catch
        {
            return ServiceResponseHelper.Error<CategoryDTO>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<CategoryDTO>> GetCategoryByIdAsync(Guid id)
    {
        var category = await _repository.GetCategoryByIdAsync(id);
        if (category is null)
            return ServiceResponseHelper.Error<CategoryDTO>(404, "Requisição inválida, Categoria não encontrada!");

        return ServiceResponseHelper.Success(200, "Retornado com sucesso", new CategoryDTO(category.Id, category.Name));
    }

    public async Task<ServiceResponse<IEnumerable<CategoryDTO>>> GetCategoriesAsync()
    {
        var categories = await _repository.GetCategoryAsync();
        List<CategoryDTO> categoriesDTO = new();
        if (!categories.Any())
            return ServiceResponseHelper.Error<IEnumerable<CategoryDTO>>(404, "Requisição inválida, Nenhuma Categoria não encontrada!");
        foreach (var category in categories)
            categoriesDTO.Add(category);
        return ServiceResponseHelper.Success(200, "Retornado com sucesso", (IEnumerable<CategoryDTO>)categoriesDTO);
    }
}
