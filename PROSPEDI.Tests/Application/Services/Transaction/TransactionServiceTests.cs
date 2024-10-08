﻿using Moq;
using PROSPERID.Application.DTOs.Category;
using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Application.Services.Transaction;
using PROSPERID.Core.Enums;
using PROSPERID.Core.Interface.Repositories;

namespace PROSPERID.Tests.Application.Services;

public class TransactionServiceTests
{
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
    private readonly TransactionService _transactionService;

    public TransactionServiceTests()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _transactionService = new TransactionService(_transactionRepositoryMock.Object, _categoryRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateTransaction_ValidTransactionPayment_ReturnsSuccessResponse()
    {
        //Arrange
        CategoryDTO categoryDTO = new(10, "Casa");
        var createTransactionDTO = new CreateTransactionDTO("transactionTeste", ETransactionType.Payment, -100, DateTime.Now,
            DateTime.Now.AddMonths(1), categoryDTO.Id);

        //Act
        var result = await _transactionService.CreateTransactionAsync(createTransactionDTO);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.Equal("Movimento criado com sucesso!", result.Message);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task CreateTransaction_ValidTransactionReceipt_ReturnsSuccessResponse()
    {
        //Arrange
        var categoryDTO = new CategoryDTO(10, "Casa");
        var createTransactionDTO = new CreateTransactionDTO("transactionTeste", ETransactionType.Payment, -100, DateTime.Now,
            DateTime.Now.AddMonths(1), categoryDTO.Id);

        //Act
        var result = await _transactionService.CreateTransactionAsync(createTransactionDTO);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.Equal("Movimento criado com sucesso!", result.Message);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task CreateTransaction_DuplicateTransaction_ShouldReturnErrorResponse()
    {
        //Arrange
        _transactionRepositoryMock.Setup(repo => repo.ExistsTransaction(It.IsAny<Core.Entities.Transaction>()))
                .ReturnsAsync(true);

        var categoryDTO = new CategoryDTO(10, "Casa");
        var createTransactionDTO = new CreateTransactionDTO("transactionTeste", ETransactionType.Payment, -100, DateTime.Now,
            DateTime.Now.AddMonths(1), categoryDTO.Id);

        //Act
        var result = await _transactionService.CreateTransactionAsync(createTransactionDTO);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.Status);
        Assert.NotEmpty(result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task UpdateTransaction_ValidTransaction_ShouldReturnSuccessResponse()
    {
        //Arrange
        var category = new Core.Entities.Category("Casa");
        Core.Entities.Transaction transaction = new("transactionTeste", category, ETransactionType.Payment, -100, DateTime.Now, DateTime.Now.AddMonths(1));
        _transactionRepositoryMock.Setup(repo => repo.GetTransactionByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(transaction);
        Core.Entities.Transaction transactionUpdate = new("updateTransactionTeste", category, ETransactionType.Payment, -100, DateTime.Now, DateTime.Now.AddMonths(1));
        _transactionRepositoryMock.Setup(repo => repo.UpdateTransactionAsync(It.IsAny<Core.Entities.Transaction>()))
                .ReturnsAsync(transactionUpdate);
        var categoryDTO = new CategoryDTO(10, "Casa");
        var updateTransactionDTO = new UpdateTransactionDTO(10, "updateTransactionTeste",
            ETransactionType.Payment, -100, DateTime.Now, DateTime.Now.AddMonths(1), categoryDTO.Id);

        //Act
        var result = await _transactionService.UpdateTransactionAsync(categoryDTO.Id, updateTransactionDTO);

        //Assert
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Message);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task UpdateTransaction_NonExistentTransaction_ShouldReturnNotFoundResponse()
    {
        //Arrange
        _transactionRepositoryMock.Setup(repo => repo.GetTransactionByIdAsync(It.IsAny<long>()))
                .ReturnsAsync((Core.Entities.Transaction)null!);
        var categoryDTO = new CategoryDTO(10, "Casa");
        var updateTransactionDTO = new UpdateTransactionDTO(10, "UpdateTeste",
            ETransactionType.Payment, 100, DateTime.Now, DateTime.Now.AddMonths(1), categoryDTO.Id);

        //Act
        var result = await _transactionService.UpdateTransactionAsync(categoryDTO.Id, updateTransactionDTO);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.Status);
        Assert.NotEmpty(result.Message);
        Assert.Null(result.Data);
    }

    //[Fact]
    //public async Task DeleteTransaction_ValidTransaction_ShouldReturnSuccessResponse()
    //{
    //    //Arrange
    //    var category = new PROSPERID.Domain.Entities.Category("Casa");
    //    PROSPERID.Domain.Entities.Transaction transaction = new("transactionTeste", category, ETransactionType.Payment, -100, DateTime.Now, DateTime.Now.AddMonths(1));
    //    _mockRepository.Setup(repo => repo.GetTransactionByIdAsync(It.IsAny<Guid>()))
    //            .ReturnsAsync(transaction);
    //    _mockRepository.Setup(repo => repo.DeleteTransactionAsync(It.IsAny<Guid>()))
    //            .ReturnsAsync((PROSPERID.Domain.Entities.Transaction)null!);
    //    var transactionService = new TransactionService(_mockRepository.Object);
    //    //Act
    //    var result = await transactionService.DeleteTransactionAsync(transaction.Id);

    //    //Assert
    //    Assert.Equal(200, result.Status);
    //    Assert.NotNull(result.Message);
    //}

    //[Fact]
    //public async Task DeleteTransaction_NonExistentTransaction_ShouldReturnNotFoundResponse()
    //{
    //    //Arrange
    //    _mockRepository.Setup(repo => repo.GetTransactionByIdAsync(It.IsAny<Guid>()))
    //            .ReturnsAsync((PROSPERID.Domain.Entities.Transaction)null!);
    //    var transactionService = new TransactionService(_mockRepository.Object);

    //    //Act
    //    var result = await transactionService.DeleteTransactionAsync(Guid.NewGuid());

    //    //Assert
    //    Assert.NotNull(result);
    //    Assert.Equal(404, result.Status);
    //    Assert.NotEmpty(result.Message);
    //    Assert.Null(result.Data);
    //}

    //[Fact]
    //public async Task DeleteTransaction_PaidTransaction_ShouldReturnErrorResponse()
    //{
    //    //Arrange
    //    PROSPERID.Domain.Entities.Category category = new("Casa");
    //    PROSPERID.Domain.Entities.BankAccount bankAccount = new("123456", "Holder_Teste", 200);
    //    PROSPERID.Domain.Entities.Transaction transaction = new("transactionTeste", category, ETransactionType.Payment, -100, DateTime.Now, DateTime.Now.AddMonths(1));
    //    transaction.ExecutePayment(bankAccount, DateTime.Now.AddMonths(1));
    //    _mockRepository.Setup(repo => repo.GetTransactionByIdAsync(It.IsAny<Guid>()))
    //            .ReturnsAsync(transaction);
    //    var transactionService = new TransactionService(_mockRepository.Object);

    //    //Act
    //    var result = await transactionService.DeleteTransactionAsync(transaction.Id);

    //    //Assert
    //    Assert.Equal(400, result.Status);
    //    Assert.NotNull(result.Message);
    //    Assert.Null(result.Data);
    //}
}
