using Moq;
using PROSPERID.Application.DTOs.Category;
using PROSPERID.Application.Services.Category;
using PROSPERID.Core.Interface.Repositories;

namespace PROSPERID.Tests.Application.Services;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _mockRepository;
    public CategoryServiceTests()
    {
        _mockRepository = new();
    }
    [Fact]
    public async Task CreateCategory_ValidCategory_ReturnsSuccessResponse()
    {
        //Arrange
        Core.Entities.Category category = new("TestCategory");
        _mockRepository.Setup(repo => repo.AnyCategoryAsync(It.IsAny<string>())).ReturnsAsync(false);
        var categoryService = new CategoryService(_mockRepository.Object);
        _mockRepository.Setup(repo => repo.CreateCategoryAsync(It.IsAny<Core.Entities.Category>()))
            .ReturnsAsync(category);
        var createCategoryDTO = new CreateCategoryDTO("TestCategory");

        //Act
        var result = await categoryService.CreateCategoryAsync(createCategoryDTO);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.Equal("Categoria criada com sucesso!", result.Message);
        Assert.NotNull(result.Data);
        Assert.Equal("TestCategory", result.Data.Name);
    }

    [Fact]
    public async Task CreateCategory_InvalidCategory_ReturnsErrorResponse()
    {
        //Arrange
        var categoryService = new CategoryService(_mockRepository.Object);
        var createCategoryDTO = new CreateCategoryDTO("");

        //Act
        var result = await categoryService.CreateCategoryAsync(createCategoryDTO);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.Status);
        Assert.Contains("Requisição inválida", result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateCategory_ValidCategory_ReturnsSuccessResponse()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.AnyCategoryAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
        _mockRepository.Setup(repo => repo.GetCategoryByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Core.Entities.Category("ExistingCategory"));

        var categoryService = new CategoryService(_mockRepository.Object);
        var updateCategoryDTO = new UpdateCategoryDTO(Guid.NewGuid(), "UpdatedCategory");

        //Act
        var result = await categoryService.UpdateCategoryAsync(updateCategoryDTO);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.Equal("Categoria atualizada com sucesso!", result.Message);
        Assert.NotNull(result.Data);
        Assert.Equal("UpdatedCategory", result.Data.Name);
    }

    [Fact]
    public async Task UpdateCategory_NotFoundCategory_ReturnsNotFoundResponse()
    {
        //Arange
        _mockRepository.Setup(repo => repo.GetCategoryByIdAsync(Guid.NewGuid()))
            .ReturnsAsync((Core.Entities.Category)null!);
        var categoryService = new CategoryService(_mockRepository.Object);
        var updateCategoryDTO = new UpdateCategoryDTO(Guid.NewGuid(), "UpdatedCategory");

        //Act
        var result = await categoryService.UpdateCategoryAsync(updateCategoryDTO);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.Status);
        Assert.Contains("Categoria não encontrada", result.Message);
    }

    [Fact]
    public async Task DeleteCategory_ValidCategory_ReturnsSuccessResponse()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.AnyCategoryAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
        _mockRepository.Setup(repo => repo.GetCategoryByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Core.Entities.Category("ExistingCategory"));
        var categoryService = new CategoryService(_mockRepository.Object);

        //Act
        var result = await categoryService.DeleteCategoryAsync(Guid.NewGuid());
        //Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.Equal("Categoria deletada!", result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task DeleteCategory_NotFoundCategory_ReturnsNotFoundResponse()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.GetCategoryByIdAsync(Guid.NewGuid()))
            .ReturnsAsync((Core.Entities.Category)null!);
        var categoryService = new CategoryService(_mockRepository.Object);
        //Act
        var result = await categoryService.DeleteCategoryAsync(Guid.NewGuid());
        //Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.Status);
        Assert.Contains("Categoria não encontrada", result.Message);
    }
}
