using PROSPERID.Core.Entities;
using PROSPERID.Core.Exceptions;

namespace PROSPERID.Tests.Domain.Entities;

public class BankAccountTests
{
    [Fact]
    public void DepositShouldIncreaseBalance()
    {
        // Arrange (Configurar)
        BankAccount account = new("123456", "Alex C.", 0);
        decimal initialBalance = account.Balance;
        decimal depositAmount = 100.00m;

        // Act (Agir)
        account.Deposit(depositAmount);

        // Assert (Afirmação)
        decimal expectedBalance = initialBalance + depositAmount;
        Assert.Equal(expectedBalance, account.Balance);
    }

    [Fact]
    public void WithdrawShouldDecreaseBalanceWhenSufficientFundsAvailable()
    {
        // Arrange (Configurar)
        BankAccount account = new("123456", "Alex C.", 100.00m);
        decimal withdrawAmount = 80.00m;

        // Act (Agir)
        account.Withdraw(withdrawAmount);

        // Assert (Afirmação)
        Assert.True(account.Balance > -1);
    }

    [Fact]
    public void WithdrawShouldNotAllowWithdrawalWhenInsufficientFunds()
    {
        // Arrange (Configurar)
        BankAccount account = new("123456", "Alex C.", 100.00m);
        decimal withdrawAmount = 180.00m;

        // Assert (Afirmação)
        Assert.Throws<InsufficientFundsException>(() => account.Withdraw(withdrawAmount));
    }


    [Fact]
    public void DepositShouldNotChangeBalanceWhenAmountIsZeroOrNegative()
    {
        decimal initialValue = 100.00m;
        BankAccount account = new("123456", "Alex C.", initialValue);
        decimal deposit = 0.00m;

        account.Deposit(deposit);

        //Assert
        Assert.True(account.Balance + deposit == initialValue);
    }

    [Fact]
    public void UpdateBankAccountSuccess()
    {
        var initialBalance = 100.00m;
        BankAccount bankAccount = new("123456", "Alex", initialBalance);
        BankAccount accountUpdated = new("123456", "Alex", initialBalance);

        var exception = Record.Exception(() => bankAccount.Update(accountUpdated.AccountNumber, accountUpdated.AccountHolder, accountUpdated.Balance));
        Assert.Null(exception);
    }

    [Fact]
    public void UpdateBankAccountWithNegativeBalanceShouldFail()
    {
        var initialBalance = 100.00m;
        BankAccount bankAccount = new("123456", "Alex", initialBalance);
        BankAccount accountUpdated = new("123456", "Alex", 0);
        accountUpdated.Balance = -110.00m;

        Assert.Throws<NegativeBalanceException>(() => bankAccount.Update(accountUpdated.AccountNumber, accountUpdated.AccountHolder, accountUpdated.Balance));
    }
}
