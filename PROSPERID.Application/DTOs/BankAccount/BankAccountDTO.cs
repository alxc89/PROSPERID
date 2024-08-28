namespace PROSPERID.Application.DTOs.BankAccount;

public class BankAccountDTO
{
    public BankAccountDTO(Guid id, string accountNumber, string accountHolder, decimal balance)
    {
        Id = id;
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = balance;
    }

    public Guid Id { get; set; }
    public string AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public decimal Balance { get; set; }

    public static implicit operator BankAccountDTO(Core.Entities.BankAccount bankAccount)
    {
        return new BankAccountDTO(bankAccount.Id, bankAccount.AccountNumber, bankAccount.AccountHolder, bankAccount.Balance);
    }
}