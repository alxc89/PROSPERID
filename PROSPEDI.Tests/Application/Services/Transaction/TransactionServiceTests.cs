using Moq;
using PROSPERID.Application.DTOs.Transaction;
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
}
