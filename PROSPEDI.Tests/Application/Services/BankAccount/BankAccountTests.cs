﻿using Moq;
using PROSPERID.Application.DTOs.BankAccount;
using PROSPERID.Application.Services.BankAccount;
using PROSPERID.Core.Interface.Repositories;

namespace PROSPERID.Tests.Application.Services;

public class BankAccountServiceTests
{
    private readonly Mock<IBankAccountRepository> _mockRepository;
    public BankAccountServiceTests()
    {
        _mockRepository = new();
    }

    [Fact]
    public async Task CreateBankAccount_InvalidInput_ShouldReturnErrorResponse()
    {
        //Arrange
        var createBankAccountDTO = new CreateBankAccountDTO
        {
            AccountHolder = "",
            AccountNumber = "12345",
            Balance = -100.0m
        };
        var bankAccountService = new BankAccountService(_mockRepository.Object);

        //Act
        var response = await bankAccountService.CreateBankAccountAsync(createBankAccountDTO);

        //Assert
        Assert.False(response.IsSuccess);
        Assert.Equal(400, response.Status);
        Assert.NotEmpty(response.Message);
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task CreateBankAccount_ValidInput_ShouldReturnSuccessResponse()
    {
        //Arrange
        Core.Entities.BankAccount bankAccount = new("12345", "John Doe", 1000.0m);
        var createBankAccountDTO = new CreateBankAccountDTO
        {
            AccountHolder = "John Doe",
            AccountNumber = "12345",
            Balance = 1000.0m
        };
        _mockRepository.Setup(repo => repo.CreateBankAccountAsync(It.IsAny<Core.Entities.BankAccount>()))
            .ReturnsAsync(bankAccount);
        var bankAccountService = new BankAccountService(_mockRepository.Object);

        //Act
        var response = await bankAccountService.CreateBankAccountAsync(createBankAccountDTO);

        //Assert
        Assert.True(response.IsSuccess);
        Assert.Equal(200, response.Status);
        Assert.Equal("Conta bancária criada com sucesso!", response.Message);
        Assert.NotNull(response.Data);
    }

    [Fact]
    public async Task CreateBankAccount_DuplicateAccountNumber_ShouldReturnErrorResponse()
    {
        //Arrange
        var createBankAccountDTO = new CreateBankAccountDTO
        {
            AccountHolder = "John Doe",
            AccountNumber = "12345",
            Balance = 1000.0m
        };
        _mockRepository.Setup(repo => repo.VerifyIfExistsAccount(It.IsAny<string>()))
            .ReturnsAsync(true);
        var bankAccountService = new BankAccountService(_mockRepository.Object);

        //Act
        var response = await bankAccountService.CreateBankAccountAsync(createBankAccountDTO);

        //Assert
        Assert.False(response.IsSuccess);
        Assert.Equal(400, response.Status);
        Assert.NotEmpty(response.Message);
        Assert.Equal($"Requisição inválida, Conta Bancária com o Número {createBankAccountDTO.AccountNumber} já existente", response.Message);
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task CreateBankAccount_RepositoryThrowsException_ShouldReturnErrorResponse()
    {
        //Arrange
        var createBankAccountDTO = new CreateBankAccountDTO
        {
            AccountHolder = "John Doe",
            AccountNumber = "12345",
            Balance = 1000.0m
        };
        var bankAccountService = new BankAccountService(_mockRepository.Object);

        //Act
        var response = await bankAccountService.CreateBankAccountAsync(createBankAccountDTO);

        //Assert
        Assert.False(response.IsSuccess);
        Assert.Equal(500, response.Status);
        Assert.Equal("Erro interno!", response.Message);
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task UpdateBankAccount_NonExistentBankAccount_ShouldReturnNotFoundResponse()
    {
        //Arrange
        var bankAccountService = new BankAccountService(_mockRepository.Object);
        _mockRepository.Setup(repo => repo.GetBankAccountByIdAsync(It.IsAny<long>()))
            .ReturnsAsync((Core.Entities.BankAccount)null!);
        var updateBankAccount = new UpdateBankAccountDTO(10, "12345", "NotExistAccount", 100.00m);

        //Act
        var response = await bankAccountService.UpdateBankAccountAsync(updateBankAccount);

        //Assert
        Assert.False(response.IsSuccess);
        Assert.Equal(400, response.Status);
        Assert.Equal($"Não existe a Conta Bancária {updateBankAccount.AccountNumber}", response.Message);
        Assert.NotEmpty(response.Message);
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task UpdateBankAccount_ValidBankAccount_ShouldReturnSuccessResponse()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.GetBankAccountByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(new Core.Entities.BankAccount("12345", "Holder", 100.00m));
        _mockRepository.Setup(repo => repo.UpdateBankAccountAsync(It.IsAny<Core.Entities.BankAccount>()))
            .ReturnsAsync(new Core.Entities.BankAccount("12345", "UpdateName", 100.00m));
        var bankAccountService = new BankAccountService(_mockRepository.Object);
        var updateBankAccount = new UpdateBankAccountDTO(10, "12345", "UpdateName", 100.00m);

        //Act
        var response = await bankAccountService.UpdateBankAccountAsync(updateBankAccount);

        //Assert
        Assert.True(response.IsSuccess);
        Assert.Equal(200, response.Status);
        Assert.Equal($"Conta Bancária alterada com sucesso!", response.Message);
        Assert.NotEmpty(response.Message);
        Assert.NotNull(response.Data);
    }
}