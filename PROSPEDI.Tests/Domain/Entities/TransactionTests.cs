﻿using PROSPERID.Domain.Entities;
using PROSPERID.Domain.Enums;

namespace PROSPERID.Tests.Domain.Entities;

public class TransactionTests
{
    [Fact]
    public void ExecutePaymentShouldDecreaseBalanceWhenSuccessful()
    {
        // Arrange
        decimal initialBalance = 100.00m;
        BankAccount account = new("123456", "Alex", initialBalance);
        Category category = new("Category");
        Transaction transaction =
            new("Payment", category, TransactionType.Payment, -50.00m, DateTime.Now, DateTime.Now);

        // Act
        bool result = transaction.ExecutePayment(account, DateTime.Now);

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
        Category category = new("Category");
        Transaction transaction =
            new("Payment", category, TransactionType.Payment, -50.00m, DateTime.Now, DateTime.Now.AddDays(-2));
        transaction.ExecutePayment(account, DateTime.Now);
        // Act
        bool result = transaction.ExecutePaymentCancellation(account);

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
        Category category = new("Category");
        Transaction transaction =
            new("Receipt", category, TransactionType.Receipt, 150.00m, DateTime.Now, DateTime.Now.AddDays(2));
        var result = transaction.ExecuteReceipt(account, DateTime.Now);

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
        Category category = new("Category");
        Transaction transaction =
            new("Receipt", category, TransactionType.Receipt, 150.00m, DateTime.Now, DateTime.Now.AddDays(2));
        transaction.ExecuteReceipt(account, DateTime.Now);
        // Act
        bool result = transaction.ExecuteReceiptCancellation(account);

        // Assert
        Assert.True(result);
        Assert.Equal(initialBalance, account.Balance);
    }
}
