namespace PROSPERID.Application.DTOs.BankAccount;

public record UpdateBankAccountDTO(Guid Id, string AccountNumber, string AccountHolder,
    decimal Balance);
