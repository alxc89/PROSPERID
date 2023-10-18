using PROSPERID.Application.DTOs.BankAccount;
using PROSPERID.Application.DTOs.FinancialMovement;
using PROSPERID.Application.Services.BankAccount;

namespace PROSPERID.Tests.Application.Services.BankAccount;

public class ValidateBankAccountInputTests
{
    [Fact]
    public void Validate_AllInputValid_ShouldReturnNull()
    {
        //Arrange
        var bankAccountDTO = new BankAccountDTO("123456", "HolderTest", 0, new List<FinancialMovementDTO>());
        //Act
        var validate = ValidateBankAccountInput<BankAccountDTO>.Validate(bankAccountDTO.AccountNumber, bankAccountDTO.AccountHolder, bankAccountDTO.Balance);
        //Assert
        Assert.Null(validate);
    }

    [Fact]
    public void Validate_AccountNumberIsNullOrEmpty_ShouldReturnErrorResponse()
    {
        //Arrange
        var bankAccountDTO = new BankAccountDTO("", "HolderTest", 0, new List<FinancialMovementDTO>());
        //Act
        var validate = ValidateBankAccountInput<BankAccountDTO>.Validate(bankAccountDTO.AccountNumber, bankAccountDTO.AccountHolder, bankAccountDTO.Balance);
        //Assert
        Assert.NotNull(validate);
        Assert.NotEmpty(validate.Message);
        Assert.False(validate.IsSuccess);
        Assert.Equal("Requisição inválida, Número da Conta é Obrigatório", validate.Message);
        Assert.Equal(400, validate.Status);
    }

    [Fact]
    public void Validate_AccountHolderIsNullOrEmpty_ShouldReturnErrorResponse()
    {
        //Arrange
        var bankAccountDTO = new BankAccountDTO("123456", "", 0, new List<FinancialMovementDTO>());
        //Act
        var validate = ValidateBankAccountInput<BankAccountDTO>.Validate(bankAccountDTO.AccountNumber, bankAccountDTO.AccountHolder, bankAccountDTO.Balance);
        //Assert
        Assert.NotNull(validate);
        Assert.NotEmpty(validate.Message);
        Assert.False(validate.IsSuccess);
        Assert.Equal("Requisição inválida, Proprietário da Conta é Obrigatório", validate.Message);
        Assert.Equal(400, validate.Status);
    }

    [Fact]
    public void Validate_ShortAccountNumber_ShouldReturnErrorResponse()
    {
        //Arrange
        var bankAccountDTO = new BankAccountDTO("12", "HolderTest", 0, new List<FinancialMovementDTO>());
        //Act
        var validate = ValidateBankAccountInput<BankAccountDTO>.Validate(bankAccountDTO.AccountNumber, bankAccountDTO.AccountHolder, bankAccountDTO.Balance);
        //Assert
        Assert.NotNull(validate);
        Assert.NotEmpty(validate.Message);
        Assert.False(validate.IsSuccess);
        Assert.Equal("Requisição inválida, Número da conta deve conter 3 ou mais caracteres", validate.Message);
        Assert.Equal(400, validate.Status);
    }

    [Fact]
    public void Validate_ShortAccountHolder_ShouldReturnErrorResponse()
    {
        //Arrange
        var bankAccountDTO = new BankAccountDTO("123456", "Ho", 0, new List<FinancialMovementDTO>());
        //Act
        var validate = ValidateBankAccountInput<BankAccountDTO>.Validate(bankAccountDTO.AccountNumber, bankAccountDTO.AccountHolder, bankAccountDTO.Balance);
        //Assert
        Assert.NotNull(validate);
        Assert.NotEmpty(validate.Message);
        Assert.False(validate.IsSuccess);
        Assert.Equal("Requisição inválida, Nome do Proprietário deve conter 3 ou mais caracteres", validate.Message);
        Assert.Equal(400, validate.Status);
    }

    [Fact]
    public void Validate_NegativeBalance_ShouldReturnValidationError()
    {
        //Arrange
        var bankAccountDTO = new BankAccountDTO("123456", "HolderTest", -100.00m, new List<FinancialMovementDTO>());
        //Act
        var validate = ValidateBankAccountInput<BankAccountDTO>.Validate(bankAccountDTO.AccountNumber, bankAccountDTO.AccountHolder, bankAccountDTO.Balance);
        //Assert
        Assert.NotNull(validate);
        Assert.NotEmpty(validate.Message);
        Assert.False(validate.IsSuccess);
        Assert.Equal("Requisição inválida, Saldo da conta não pode ser negativa", validate.Message);
        Assert.Equal(400, validate.Status);
    }
}
