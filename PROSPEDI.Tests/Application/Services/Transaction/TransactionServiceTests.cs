using Moq;
using PROSPERID.Application.DTOs.BankAccount;
using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Application.Services.BankAccount;
using PROSPERID.Application.Services.Transaction;
using PROSPERID.Domain.Enums;
using PROSPERID.Domain.Interface.Repositories;

namespace PROSPERID.Tests.Application.Services;

public class TransactionServiceTests
{
    private readonly Mock<ITransactionRepository> _mockRepository;
    public TransactionServiceTests()
    {
        _mockRepository = new();
    }

    [Fact]
    public async Task CreateTransaction_ValidTransactionPayment_ReturnsSuccessResponse()
    {
        //Arrange
        PROSPERID.Domain.Entities.Transaction transaction = new("transactionTeste", "Casa", TransactionType.Payment, -100, DateTime.Now, DateTime.Now.AddMonths(1));
        _mockRepository.Setup(repo => repo.CreateTransactionAsync(It.IsAny<PROSPERID.Domain.Entities.Transaction>()))
                .ReturnsAsync(transaction);
        var transactionService = new TransactionService(_mockRepository.Object);
        var createTransactionDTO = new CreateTransactionDTO("transactionTeste", "Casa", TransactionType.Payment, -100, DateTime.Now, DateTime.Now.AddMonths(1));

        //Act
        var result = await transactionService.CreateTransactionAsync(createTransactionDTO);

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
        PROSPERID.Domain.Entities.Transaction transaction = new("transactionTeste", "Casa", TransactionType.Payment, 100, DateTime.Now, DateTime.Now.AddMonths(1));
        _mockRepository.Setup(repo => repo.CreateTransactionAsync(It.IsAny<PROSPERID.Domain.Entities.Transaction>()))
                .ReturnsAsync(transaction);
        var transactionService = new TransactionService(_mockRepository.Object);
        var createTransactionDTO = new CreateTransactionDTO("transactionTeste", "Casa", TransactionType.Payment, 100, DateTime.Now, DateTime.Now.AddMonths(1));

        //Act
        var result = await transactionService.CreateTransactionAsync(createTransactionDTO);

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
        _mockRepository.Setup(repo => repo.ExistsTransaction(It.IsAny<PROSPERID.Domain.Entities.Transaction>()))
                .ReturnsAsync(true);
        var transactionService = new TransactionService(_mockRepository.Object);
        var createTransactionDTO = new CreateTransactionDTO("transactionTeste", "Casa", TransactionType.Payment, 100, DateTime.Now, DateTime.Now.AddMonths(1));

        //Act
        var result = await transactionService.CreateTransactionAsync(createTransactionDTO);

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
        PROSPERID.Domain.Entities.Transaction transaction = new("transactionTeste", "Casa", TransactionType.Payment, -100, DateTime.Now, DateTime.Now.AddMonths(1));
        _mockRepository.Setup(repo => repo.GetTransactionByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(transaction);
        PROSPERID.Domain.Entities.Transaction transactionUpdate = new("updateTransactionTeste", "Casa", TransactionType.Payment, -100, DateTime.Now, DateTime.Now.AddMonths(1));
        _mockRepository.Setup(repo => repo.UpdateTransactionAsync(It.IsAny<PROSPERID.Domain.Entities.Transaction>()))
                .ReturnsAsync(transactionUpdate);
        var transactionService = new TransactionService(_mockRepository.Object);
        var updateTransactionDTO = new UpdateTransactionDTO(Guid.NewGuid(), "updateTransactionTeste", "Casa", TransactionType.Payment, -100, DateTime.Now, DateTime.Now.AddMonths(1));

        //Act
        var result = await transactionService.UpdateTransactionAsync(updateTransactionDTO);

        //Assert
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Message);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task UpdateTransaction_NonExistentTransaction_ShouldReturnNotFoundResponse()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.GetTransactionByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((PROSPERID.Domain.Entities.Transaction)null!);
        var transactionService = new TransactionService(_mockRepository.Object);
        var updateTransactionDTO = new UpdateTransactionDTO(Guid.NewGuid(), "UpdateTeste", "Casa", TransactionType.Payment, 100, DateTime.Now, DateTime.Now.AddMonths(1));

        //Act
        var result = await transactionService.UpdateTransactionAsync(updateTransactionDTO);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.Status);
        Assert.NotEmpty(result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task DeleteTransaction_ValidTransaction_ShouldReturnSuccessResponse()
    {
        //Arrange
        PROSPERID.Domain.Entities.Transaction transaction = new("transactionTeste", "Casa", TransactionType.Payment, -100, DateTime.Now, DateTime.Now.AddMonths(1));
        _mockRepository.Setup(repo => repo.GetTransactionByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(transaction);
        _mockRepository.Setup(repo => repo.DeleteTransactionAsync(It.IsAny<Guid>()))
                .ReturnsAsync((PROSPERID.Domain.Entities.Transaction)null!);
        var transactionService = new TransactionService(_mockRepository.Object);
        //Act
        var result = await transactionService.DeleteTransactionAsync(transaction.Id);

        //Assert
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Message);
    }

    [Fact]
    public async Task DeleteTransaction_NonExistentTransaction_ShouldReturnNotFoundResponse()
    {
        //Arrange
        _mockRepository.Setup(repo => repo.GetTransactionByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((PROSPERID.Domain.Entities.Transaction)null!);
        var transactionService = new TransactionService(_mockRepository.Object);

        //Act
        var result = await transactionService.DeleteTransactionAsync(Guid.NewGuid());

        //Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.Status);
        Assert.NotEmpty(result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task DeleteTransaction_PaidTransaction_ShouldReturnErrorResponse()
    {
        //Arrange
        PROSPERID.Domain.Entities.BankAccount bankAccount = new("123456", "Holder_Teste", 200);
        PROSPERID.Domain.Entities.Transaction transaction = new("transactionTeste", "Casa", TransactionType.Payment, -100, DateTime.Now, DateTime.Now.AddMonths(1));
        transaction.ExecutePayment(bankAccount, DateTime.Now.AddMonths(1));
        _mockRepository.Setup(repo => repo.GetTransactionByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(transaction);
        var transactionService = new TransactionService(_mockRepository.Object);

        //Act
        var result = await transactionService.DeleteTransactionAsync(transaction.Id);

        //Assert
        Assert.Equal(400, result.Status);
        Assert.NotNull(result.Message);
        Assert.Null(result.Data);
    }
}
