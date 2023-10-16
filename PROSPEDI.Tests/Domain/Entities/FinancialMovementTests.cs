using PROSPERID.Domain.Entities;
using PROSPERID.Domain.Enums;

namespace PROSPERID.Tests.Domain.Entities;

public class FinancialMovementTests
{
    [Fact]
    public void ExecutePaymentShouldDecreaseBalanceWhenSuccessful()
    {
        // Arrange
        decimal initialBalance = 100.00m;
        BankAccount account = new("123456", "Alex", initialBalance);

        FinancialMovement financialMovement =
            new("Payment", "Category", TransactionType.Payment, -50.00m, DateTime.Now, DateTime.Now);

        // Act
        bool result = financialMovement.ExecutePayment(account, DateTime.Now);

        // Assert
        Assert.True(result);
        Assert.Equal(initialBalance - 50.00m, account.Balance);
    }

    [Fact]
    public void ExecutePaymentCancellationShouldReversePaymentWhenSuccessful()
    {
        // Arrange
        decimal initialBalance = 100.00m;
        BankAccount account = new("123456", "Alex", initialBalance);

        FinancialMovement financialMovement =
            new("Payment", "Category", TransactionType.Payment, -50.00m, DateTime.Now, DateTime.Now.AddDays(-2));
        financialMovement.ExecutePayment(account, DateTime.Now);
        // Act
        bool result = financialMovement.ExecutePaymentCancellation(account);

        // Assert
        Assert.True(result);
        Assert.Equal(initialBalance, account.Balance);
    }

    [Fact]
    public void ExecuteReceiptShouldIncreaseBalanceWhenSuccessful()
    {
        // Arrange
        decimal initialBalance = 100.00m;
        BankAccount account = new("123456", "Alex", initialBalance);

        FinancialMovement financialMovement =
            new("Receipt", "Category", TransactionType.Receipt, 150.00m, DateTime.Now, DateTime.Now.AddDays(2));
        var result = financialMovement.ExecuteReceipt(account, DateTime.Now);

        //Assert
        Assert.True(result);
        Assert.True(account.Balance > initialBalance);
    }

    [Fact]
    public void ExecuteReceiptCancellationShouldReverseReceiptWhenSuccessful()
    {
        // Arrange
        decimal initialBalance = 100.00m;
        BankAccount account = new("123456", "Alex", initialBalance);

        FinancialMovement financialMovement =
            new("Receipt", "Category", TransactionType.Receipt, 150.00m, DateTime.Now, DateTime.Now.AddDays(2));
        financialMovement.ExecuteReceipt(account, DateTime.Now);
        // Act
        bool result = financialMovement.ExecuteReceiptCancellation(account);

        // Assert
        Assert.True(result);
        Assert.Equal(initialBalance, account.Balance);
    }
}
