using PROSPERID.Application.Services.BankAccount;
using PROSPERID.Application.Services.Category;
using PROSPERID.Application.Services.DTOs.BankAccount;
using PROSPERID.Application.Services.DTOs.Category;
using PROSPERID.Application.Services.DTOs.FinancialMovement;

namespace PROSPERID.Tests.Application.Services.Category;

public class ValidateCategoryInputTests
{
    [Fact]
    public void Validate_AllInputValid_ShouldReturnNull()
    {
        //Arange
        var categoryDTO = new CategoryDTO(Guid.NewGuid(), "Casa");
        //Act
        var validate = ValidateCategoryInput<CategoryDTO>.Validate(categoryDTO.Name);
        //Assert
        Assert.Null(validate);
    }

    [Fact]
    public void Validate_NameIsNullOrEmpty_ShouldReturnErrorResponse()
    {
        //Arange
        var categoryDTO = new CategoryDTO(Guid.NewGuid(), "");
        //Act
        var validate = ValidateCategoryInput<CategoryDTO>.Validate(categoryDTO.Name);
        //Assert
        Assert.NotNull(validate);
        Assert.NotEmpty(validate.Message);
        Assert.False(validate.IsSuccess);
        Assert.Equal("Requisição inválida, Nome da Categoria é Obrigatório", validate.Message);
        Assert.Equal(400, validate.Status);
    }

    [Fact]
    public void Validate_ShortName_ShouldReturnErrorResponse()
    {
        //Arange
        var categoryDTO = new CategoryDTO(Guid.NewGuid(), "Cas");
        //Act
        var validate = ValidateCategoryInput<CategoryDTO>.Validate(categoryDTO.Name);
        //Assert
        Assert.NotNull(validate);
        Assert.NotEmpty(validate.Message);
        Assert.False(validate.IsSuccess);
        Assert.Equal("Requisição inválida, Categoria deve conter 4 ou mais caracteres", validate.Message);
        Assert.Equal(400, validate.Status);
    }
}
