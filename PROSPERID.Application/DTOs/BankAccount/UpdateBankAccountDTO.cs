namespace PROSPERID.Application.DTOs.BankAccount;

public record UpdateBankAccountDTO(long Id, string AccountNumber, string AccountHolder,
    decimal Balance);   
