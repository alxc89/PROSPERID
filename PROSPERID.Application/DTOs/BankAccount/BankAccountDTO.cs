using PROSPERID.Application.DTOs.FinancialMovement;

namespace PROSPERID.Application.DTOs.BankAccount;

public class BankAccountDTO
{
    public BankAccountDTO(string accountNumber, string accountHolder, decimal balance,
        List<FinancialMovementDTO> transactions)
    {
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = balance;
        Transactions = transactions;
    }

    public string AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public decimal Balance { get; set; }
    public List<FinancialMovementDTO> Transactions { get; set; } = new List<FinancialMovementDTO>();

    public static implicit operator BankAccountDTO(Domain.Entities.BankAccount bankAccount)
    {
        var listFinancialMovementDTO = new List<FinancialMovementDTO>();
        foreach (var transaction in bankAccount.Transactions)
            listFinancialMovementDTO.Add(transaction);
        return new BankAccountDTO(bankAccount.AccountNumber, bankAccount.AccountHolder, bankAccount.Balance,
            listFinancialMovementDTO);
    }
}