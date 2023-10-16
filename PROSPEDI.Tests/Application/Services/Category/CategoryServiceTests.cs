using PROSPERID.Application.Services.Category;
using PROSPERID.Application.Services.DTOs.Category;
using PROSPERID.Domain.Interface.Repositories;
using Moq;
using PROSPERID.Domain.Entities;

namespace PROSPERID.Tests.Application.Services;

public class CategoryServiceTests
{
    [Fact]
    public async Task CreateCategoryValidCategoryReturnsSuccessResponse()
    {
        // Arrange
        PROSPERID.Domain.Entities.Category category = new("TestCategory");
        var mockRepository = new Mock<ICategoryRepository>();
        mockRepository.Setup(repo => repo.AnyCategoryAsync(It.IsAny<string>())).ReturnsAsync(false);
        var categoryService = new CategoryService(mockRepository.Object);
        mockRepository.Setup(repo => repo.CreateCategoryAsync(It.IsAny<PROSPERID.Domain.Entities.Category>())).ReturnsAsync(category);
        var createCategoryDTO = new CreateCategoryDTO("TestCategory");

        // Act
        var result = await categoryService.CreateCategoryAsync(createCategoryDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.Equal("Categoria criada com sucesso!", result.Message);
        Assert.NotNull(result.Data);
        Assert.Equal("TestCategory", result.Data.Name);
    }

    [Fact]
    public async Task CreateCategoryInvalidCategoryReturnsErrorResponse()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();

        var categoryService = new CategoryService(mockRepository.Object);
        var createCategoryDTO = new CreateCategoryDTO(""); // Categoria inválida

        // Act
        var result = await categoryService.CreateCategoryAsync(createCategoryDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.Status);
        Assert.Contains("Requisição inválida", result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateCategoryValidCategoryReturnsSuccessResponse()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        mockRepository.Setup(repo => repo.AnyCategoryAsync(It.IsAny<string>())).ReturnsAsync(false);
        mockRepository.Setup(repo => repo.GetCategoryByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new PROSPERID.Domain.Entities.Category("ExistingCategory"));

        var categoryService = new CategoryService(mockRepository.Object);
        var updateCategoryDTO = new UpdateCategoryDTO(Guid.NewGuid(), "UpdatedCategory");

        // Act
        var result = await categoryService.UpdateCategoryAsync(updateCategoryDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.Equal("Categoria atualizada com sucesso!", result.Message);
        Assert.NotNull(result.Data);
        Assert.Equal("UpdatedCategory", result.Data.Name);
    }

    [Fact]
    public async Task UpdateCategoryIdNotFoundReturnsNotFoundResponse()
    {
        var mockRepository = new Mock<ICategoryRepository>();
        // Configurar um método personalizado para GetCategoryByIdAsync
        mockRepository.Setup(repo => repo.GetCategoryByIdAsync(Guid.NewGuid())).ReturnsAsync((PROSPERID.Domain.Entities.Category)null!);
        var categoryService = new CategoryService(mockRepository.Object);
        var updateCategoryDTO = new UpdateCategoryDTO(Guid.NewGuid(), "UpdatedCategory");

        //Act: Tentar excluir uma categoria com um id que não existe
        var result = await categoryService.UpdateCategoryAsync(updateCategoryDTO);
        // Assert: Verificar que o resultado é uma resposta de "Not Found"
        Assert.NotNull(result);
        Assert.Equal(404, result.Status);
        Assert.Contains("Categoria não encontrada", result.Message);
    }

    [Fact]
    public async Task DeleteCategoryValidCategoryReturnsSuccessResponse()
    {
        // Arrange
        var mockRepository = new Mock<ICategoryRepository>();
        mockRepository.Setup(repo => repo.AnyCategoryAsync(It.IsAny<string>())).ReturnsAsync(false);
        mockRepository.Setup(repo => repo.GetCategoryByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new PROSPERID.Domain.Entities.Category("ExistingCategory"));
        var categoryService = new CategoryService(mockRepository.Object);

        //Act
        var result = await categoryService.DeleteCategoryAsync(Guid.NewGuid());
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.Equal("Categoria deletada!", result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task DeleteCategoryIdNotFoundReturnsNotFoundResponse()
    {
        var mockRepository = new Mock<ICategoryRepository>();
        // Configurar um método personalizado para GetCategoryByIdAsync
        mockRepository.Setup(repo => repo.GetCategoryByIdAsync(Guid.NewGuid())).ReturnsAsync((PROSPERID.Domain.Entities.Category)null!);
        var categoryService = new CategoryService(mockRepository.Object);
        //Act: Tentar excluir uma categoria com um id que não existe
        var result = await categoryService.DeleteCategoryAsync(Guid.NewGuid());
        // Assert: Verificar que o resultado é uma resposta de "Not Found"
        Assert.NotNull(result);
        Assert.Equal(404, result.Status);
        Assert.Contains("Categoria não encontrada", result.Message);
    }
    // Adicione mais testes para cenários de erro e outros métodos conforme necessário.
}
