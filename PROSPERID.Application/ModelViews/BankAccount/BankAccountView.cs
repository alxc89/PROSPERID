namespace PROSPERID.Application.ModelViews.BankAccount;

public class BankAccountView(long id, string accountNumber, string accountHolder, decimal balance)
{
    /// <summary>
    /// Id da Conta Bancária
    /// </summary>
    /// <example>1</example>
    public long Id { get; set; } = id;

    /// <summary>
    /// Numero Conta Bancária
    /// </summary>
    /// <example>123456-5</example>
    public string AccountNumber { get; set; } = accountNumber;

    /// <summary>
    /// Proprietário da Conta Bancária
    /// </summary>
    /// <example>José da Silva</example>
    public string AccountHolder { get; set; } = accountHolder;

    /// <summary>
    /// Numero Conta Bancária
    /// </summary>
    /// <example>1000.00</example>
    public decimal Balance { get; set; } = balance;

    public static implicit operator BankAccountView(Core.Entities.BankAccount bankAccount)
    {
        return new BankAccountView(bankAccount.Id, bankAccount.AccountNumber, bankAccount.AccountHolder, bankAccount.Balance);
    }
}
