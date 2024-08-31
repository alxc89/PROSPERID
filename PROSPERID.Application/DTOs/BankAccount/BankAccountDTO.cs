namespace PROSPERID.Application.DTOs.BankAccount;

public class BankAccountDTO(long id, string accountNumber, string accountHolder, decimal balance)
{
    public long Id { get; set; } = id;
    public string AccountNumber { get; set; } = accountNumber;
    public string AccountHolder { get; set; } = accountHolder;
    public decimal Balance { get; set; } = balance;

    public static implicit operator BankAccountDTO(Core.Entities.BankAccount bankAccount)
    {
        return new BankAccountDTO(bankAccount.Id, bankAccount.AccountNumber, bankAccount.AccountHolder, bankAccount.Balance);
    }
}