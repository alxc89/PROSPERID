namespace PROSPERID.Application.Services.DTOs.BankAccount;
public record CreateBankAccountDTO(string AccountNumber, string AccountHolder,
    decimal Balance);
