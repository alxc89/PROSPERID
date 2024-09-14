namespace PROSPERID.Tests.Application.Services.Transaction;

public class ValidateTransactionInputTests
{
    //[Fact]
    //public void Validate_AllInputValid_ShouldReturnNull()
    //{
    //    //Arrange
    //    var categoryDTO = new CategoryDTO(Guid.NewGuid(), "Carro");
    //    var transactionDTO = new TransactionDTO("Pagamento Carro", categoryDTO,
    //        ETransactionType.Payment, -850.00m, DateTime.Now,
    //        DateTime.Now.AddDays(2));
    //    //Act
    //    var validate = ValidateTransaction<TransactionDTO>.Validate(transactionDTO);
    //    //Assert
    //    Assert.Null(validate);
    //}

    //[Fact]
    //public void Validate_DescriptionEmpty_ShouldReturnErrorResponse()
    //{
    //    //Arrange
    //    var categoryDTO = new CategoryDTO(Guid.NewGuid(), "Carro");
    //    var transactionDTO = new TransactionDTO("", categoryDTO,
    //        ETransactionType.Payment, -850.00m, DateTime.Now,
    //        DateTime.Now.AddDays(2));
    //    //Act
    //    var validate = ValidateTransaction<TransactionDTO>.Validate(transactionDTO);
    //    //Assert
    //    Assert.NotNull(validate);
    //    Assert.NotEmpty(validate.Message);
    //    Assert.False(validate.IsSuccess);
    //    Assert.Equal(400, validate.Status);
    //}

    //[Fact]
    //public void Validate_DescriptionNull_ShouldReturnErrorResponse()
    //{
    //    //Arrange
    //    var categoryDTO = new CategoryDTO(Guid.NewGuid(), "Carro");
    //    var transactionDTO = new TransactionDTO(null!, categoryDTO,
    //        ETransactionType.Payment, -850.00m, DateTime.Now,
    //        DateTime.Now.AddDays(2));
    //    //Act
    //    var validate = ValidateTransaction<TransactionDTO>.Validate(transactionDTO);
    //    //Assert
    //    Assert.NotNull(validate);
    //    Assert.NotEmpty(validate.Message);
    //    Assert.False(validate.IsSuccess);
    //    Assert.Equal(400, validate.Status);
    //}
}
