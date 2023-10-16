using Moq;
using PROSPERID.Application.Services.DTOs.BankAccount;
using PROSPERID.Domain.Interface.Repositories;
using PROSPERID.Application.Services.BankAccount;

namespace PROSPERID.Tests.Application.Services;

public class BankAccountServiceTests
{
    [Fact]
    public async Task CreateBankAccount_InvalidInput_ShouldReturnErrorResponse()
    {
        // Arrange
        var createBankAccountDTO = new CreateBankAccountDTO("12345", "", -100.0m);
        var mockRepository = new Mock<IBankAccountRepository>();
        var bankAccountService = new BankAccountService(mockRepository.Object);

        // Act
        var response = await bankAccountService.CreateBankAccountAsync(createBankAccountDTO);

        // Assert
        Assert.False(response.IsSuccess);
        Assert.Equal(400, response.Status);
        Assert.NotEmpty(response.Message);
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task CreateBankAccount_ValidInput_ShouldReturnSuccessResponse()
    {
        // Arrange
        PROSPERID.Domain.Entities.BankAccount bankAccount = new("12345", "John Doe", 1000.0m);
        var createBankAccountDTO = new CreateBankAccountDTO("12345", "John Doe", 1000.0m);
        var mockRepository = new Mock<IBankAccountRepository>();
        mockRepository.Setup(repo => repo.CreateBankAccountAsync(It.IsAny<PROSPERID.Domain.Entities.BankAccount>()))
            .ReturnsAsync(bankAccount);
        var bankAccountService = new BankAccountService(mockRepository.Object);

        // Act
        var response = await bankAccountService.CreateBankAccountAsync(createBankAccountDTO);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Equal(200, response.Status);
        Assert.Equal("Conta bancária criada com sucesso!", response.Message);
        Assert.NotNull(response.Data);
    }

    [Fact]
    public async Task CreateBankAccount_DuplicateAccountNumber_ShouldReturnErrorResponse()
    {
        // Arrange
        var createBankAccountDTO = new CreateBankAccountDTO("12345", "John Doe", 1000.0m);
        var mockRepository = new Mock<IBankAccountRepository>();
        mockRepository.Setup(repo => repo.VerifyIfExistsAccount(It.IsAny<string>()))
            .ReturnsAsync(true);
        var bankAccountService = new BankAccountService(mockRepository.Object);

        // Act
        var response = await bankAccountService.CreateBankAccountAsync(createBankAccountDTO);

        // Assert
        Assert.False(response.IsSuccess);
        Assert.Equal(400, response.Status);
        Assert.NotEmpty(response.Message);
        Assert.Equal($"Requisição inválida, Conta Bancária com o Número {createBankAccountDTO.AccountNumber} já existente", response.Message);
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task CreateBankAccount_RepositoryThrowsException_ShouldReturnErrorResponse()
    {
        // Arrange
        var createBankAccountDTO = new CreateBankAccountDTO("12345", "John Doe", 1000.0m);
        var mockRepository = new Mock<IBankAccountRepository>();
        var bankAccountService = new BankAccountService(mockRepository.Object);

        // Act
        var response = await bankAccountService.CreateBankAccountAsync(createBankAccountDTO);

        // Assert
        Assert.False(response.IsSuccess);
        Assert.Equal(500, response.Status);
        Assert.Equal("Erro interno!", response.Message);
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task UpdateBankAccount_NonExistentBankAccount_ShouldReturnNotFoundResponse()
    {
        // Arrange
        var mockRepository = new Mock<IBankAccountRepository>();
        var bankAccountService = new BankAccountService(mockRepository.Object);
        mockRepository.Setup(repo => repo.GetBankAccountByIdAsync(It.IsAny<Guid>())).ReturnsAsync((PROSPERID.Domain.Entities.BankAccount)null!);
        var updateBankAccount = new UpdateBankAccountDTO(Guid.NewGuid(), "12345", "NotExistAccount", 100.00m);

        // Act
        var response = await bankAccountService.UpdateAccountAsync(updateBankAccount);

        // Assert
        Assert.False(response.IsSuccess);
        Assert.Equal(400, response.Status);
        Assert.Equal($"Não existe a Conta Bancária {updateBankAccount.AccountNumber}", response.Message);
        Assert.NotEmpty(response.Message);
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task UpdateBankAccount_ValidBankAccount_ShouldReturnErrorResponse()
    {
        // Arrange
        var mockRepository = new Mock<IBankAccountRepository>();
        mockRepository.Setup(repo => repo.GetBankAccountByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new PROSPERID.Domain.Entities.BankAccount("12345", "Holder", 100.00m));
        mockRepository.Setup(repo => repo.UpdateBankAccountAsync(It.IsAny<PROSPERID.Domain.Entities.BankAccount>()))
            .ReturnsAsync(new PROSPERID.Domain.Entities.BankAccount("12345", "UpdateName", 100.00m));
        var bankAccountService = new BankAccountService(mockRepository.Object);
        var updateBankAccount = new UpdateBankAccountDTO(Guid.NewGuid(), "12345", "UpdateName", 100.00m);

        // Act
        var response = await bankAccountService.UpdateAccountAsync(updateBankAccount);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.Equal(200, response.Status);
        Assert.Equal($"Conta Bancária alterada com sucesso!", response.Message);
        Assert.NotEmpty(response.Message);
        Assert.NotNull(response.Data);
    }
}