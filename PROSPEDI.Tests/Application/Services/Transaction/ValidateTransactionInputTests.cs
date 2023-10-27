using PROSPERID.Application.DTOs.Category;
using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Application.Services.Category;
using PROSPERID.Application.Services.Transaction;
using PROSPERID.Domain.Enums;

namespace PROSPERID.Tests.Application.Services.Transaction;

public class ValidateTransactionInputTests
{
    [Fact]
    public void Validate_AllInputValid_ShouldReturnNull()
    {
        //Arrange
        var transactionDTO = new TransactionDTO("Pagamento Carro", "Carro",
            TransactionType.Payment, -850.00m, DateTime.Now,
            DateTime.Now.AddDays(2));
        //Act
        var validate = ValidateTransaction<TransactionDTO>.Validate(transactionDTO);
        //Assert
        Assert.Null(validate);
    }

    [Fact]
    public void Validate_DescriptionEmpty_ShouldReturnErrorResponse()
    {
        //Arrange
        var transactionDTO = new TransactionDTO("", "Carro",
            TransactionType.Payment, -850.00m, DateTime.Now,
            DateTime.Now.AddDays(2));
        //Act
        var validate = ValidateTransaction<TransactionDTO>.Validate(transactionDTO);
        //Assert
        Assert.NotNull(validate);
        Assert.NotEmpty(validate.Message);
        Assert.False(validate.IsSuccess);
        Assert.Equal(400, validate.Status);
    }

    [Fact]
    public void Validate_DescriptionNull_ShouldReturnErrorResponse()
    {
        //Arrange
        var transactionDTO = new TransactionDTO(null!, "Carro",
            TransactionType.Payment, -850.00m, DateTime.Now,
            DateTime.Now.AddDays(2));
        //Act
        var validate = ValidateTransaction<TransactionDTO>.Validate(transactionDTO);
        //Assert
        Assert.NotNull(validate);
        Assert.NotEmpty(validate.Message);
        Assert.False(validate.IsSuccess);
        Assert.Equal(400, validate.Status);
    }
}
