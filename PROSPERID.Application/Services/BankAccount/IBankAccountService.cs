using PROSPERID.Application.Services.DTOs.BankAccount;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.BankAccount;

public interface IBankAccountService
{
    Task<ServiceResponse<BankAccountDTO>> CreateBankAccountAsync(CreateBankAccountDTO createCategoryDTO);
    Task<ServiceResponse<BankAccountDTO>> UpdateAccountAsync(UpdateBankAccountDTO updateBankAccountDTO);
    Task<ServiceResponse<BankAccountDTO>> GetBankAccountByIdAsync(Guid id);
    Task<ServiceResponse<IEnumerable<BankAccountDTO>>> GetBankAccountsAsync();
    Task<ServiceResponse<BankAccountDTO>> DeleteBankAccountAsync(Guid id);
}
