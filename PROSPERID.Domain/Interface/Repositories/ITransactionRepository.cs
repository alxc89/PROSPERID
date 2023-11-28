using PROSPERID.Domain.Entities;

namespace PROSPERID.Domain.Interface.Repositories;

public interface ITransactionRepository
{
    Task<Transaction> CreateTransactionAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetTransactionsAsync();
    Task<Transaction> GetTransactionByIdAsync(Guid id);
    Task<Transaction> UpdateTransactionAsync(Transaction transaction);
    Task<Transaction> DeleteTransactionAsync(Guid id);
    Task<bool> ExistsTransaction(Transaction transaction);
}
