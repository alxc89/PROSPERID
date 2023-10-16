using PROSPERID.Application.Services.DTOs.FinancialMovement;

namespace PROSPERID.Application.Services.DTOs.BankAccount;

public record UpdateBankAccountDTO(Guid Id, string AccountNumber, string AccountHolder,
    decimal Balance);
