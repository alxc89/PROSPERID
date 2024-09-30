using PROSPERID.Application.DTOs.BankAccount;
using PROSPERID.Application.ModelViews.BankAccount;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.BankAccount;

public interface IBankAccountService
{
    Task<ServiceResponse<BankAccountView>> GetBankAccountByIdAsync(long id);
    Task<ServiceResponse<IEnumerable<BankAccountView>>> GetBankAccountsAsync();
    Task<ServiceResponse<BankAccountView>> CreateBankAccountAsync(CreateBankAccountDTO createCategoryDTO);
    Task<ServiceResponse<BankAccountView>> UpdateBankAccountAsync(UpdateBankAccountDTO updateBankAccountDTO);
    Task<ServiceResponse<BankAccountView>> DeleteBankAccountAsync(long id);
}
