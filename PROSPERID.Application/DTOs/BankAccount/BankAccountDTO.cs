using PROSPERID.Application.DTOs.Transaction;

namespace PROSPERID.Application.DTOs.BankAccount;

public class BankAccountDTO
{
    public BankAccountDTO(string accountNumber, string accountHolder, decimal balance,
        List<TransactionDTO> transactions)
    {
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = balance;
        Transactions = transactions;
    }

    public string AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public decimal Balance { get; set; }
    public List<TransactionDTO> Transactions { get; set; } = new List<TransactionDTO>();

    public static implicit operator BankAccountDTO(Domain.Entities.BankAccount bankAccount)
    {
        var listTransactionDTO = new List<TransactionDTO>();
        foreach (var transaction in bankAccount.Transactions)
            listTransactionDTO.Add(transaction);
        return new BankAccountDTO(bankAccount.AccountNumber, bankAccount.AccountHolder, bankAccount.Balance,
            listTransactionDTO);
    }
}