using PROSPERID.Domain.Exceptions;

namespace PROSPERID.Domain.Entities;

public class BankAccount : Entity
{
    public string AccountNumber { get; set; } // Número da conta bancária
    public string AccountHolder { get; set; } // Nome do titular da conta
    public decimal Balance { get; set; } // Saldo da conta
    public List<Transaction> Transactions { get; set; } // Lista de transações relacionadas à conta bancária

    // Construtor
    public BankAccount(string accountNumber, string accountHolder, decimal initialValue)
    {
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Deposit(initialValue);
        Transactions = new List<Transaction>();
    }

    public void Update(string accountNumber, string accountHolder, decimal balance)
    {
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        if (balance < 0)
            throw new NegativeBalanceException();
        Balance = balance;
    }

    public void Deposit(decimal amount)
    {
        if (amount > 0)
            Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (!IsBalanceSufficient(amount))
            throw new InsufficientFundsException();
        Balance -= amount;
    }

    public bool MakePayment(decimal amount)
    {
        if (!IsBalanceSufficient(amount))
            return false;
        Balance -= amount;
        return true;
    }

    private bool IsBalanceSufficient(decimal amount)
        => Balance >= amount;
}
