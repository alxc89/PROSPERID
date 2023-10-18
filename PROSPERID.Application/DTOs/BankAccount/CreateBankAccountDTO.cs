namespace PROSPERID.Application.DTOs.BankAccount;

public record CreateBankAccountDTO(string AccountNumber, string AccountHolder,
    decimal Balance);
