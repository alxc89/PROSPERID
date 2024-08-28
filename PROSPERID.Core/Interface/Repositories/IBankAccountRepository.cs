using PROSPERID.Core.Entities;

namespace PROSPERID.Core.Interface.Repositories;

public interface IBankAccountRepository
{
    Task<BankAccount> CreateBankAccountAsync(BankAccount bankAccount);
    Task<IEnumerable<BankAccount>> GetBankAccountsAsync();
    Task<BankAccount?> GetBankAccountByIdAsync(Guid id);
    Task<bool> AnyBankAccountAsync(string accountNumber);
    Task<BankAccount?> UpdateBankAccountAsync(BankAccount bankAccount);
    Task DeleteBankAccountAsync(Guid id);
    Task<bool> AnyMovementInAccount(string accountNumber);
    Task<bool> VerifyIfExistsAccount(string accountNumber);
}
