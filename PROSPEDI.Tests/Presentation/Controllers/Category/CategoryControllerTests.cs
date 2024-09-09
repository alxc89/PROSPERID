using Microsoft.AspNetCore.Mvc;
using Moq;
using PROSPERID.Application.DTOs.Category;
using PROSPERID.Application.ModelViews.Category;
using PROSPERID.Application.Services.Category;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Presentation.Controllers.Category;

namespace PROSPERID.Tests.Presentation.Controllers.Category;

public class CategoryControllerTests
{
    [Fact]
    public async Task CreateAsync_ValidCategory_ReturnsOk()
    {
        // Arrange
        var createCategoryDTO = new CreateCategoryDTO("Test Category");
        var categoryView = new CategoryView(10, "Test Category");
        ServiceResponse<CategoryView> serviceResponse = ServiceResponseHelper
            .Success(200, "Categoria criada com sucesso!", categoryView);
        var categoryServiceMock = new Mock<ICategoryService>();

        categoryServiceMock
            .Setup(serv => serv.CreateCategoryAsync(It.IsAny<CreateCategoryDTO>()))
            .ReturnsAsync(serviceResponse); // Aqui você está retornando a variável serviceResponse

        var controller = new CategoryController(categoryServiceMock.Object);

        // Act
        var result = await controller.Post(createCategoryDTO) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        // Optionally, you can further assert the content of the response
    }
}
