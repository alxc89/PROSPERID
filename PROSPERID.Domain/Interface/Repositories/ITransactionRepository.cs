using PROSPERID.Domain.Entities;

namespace PROSPERID.Domain.Interface.Repositories;

public interface ITransactionRepository
{
    Task<Transaction> CreateTransactionAsync(Transaction Transaction);
    Task<IEnumerable<Transaction>> GetTransactionsAsync();
    Task<Transaction> GetTransactionByIdAsync(Guid id);
    Task<Transaction> UpdateTransactionAsync(Transaction Transaction);
    Task<Transaction> DeleteTransactionAsync(Guid id);
    Task<bool> ExistsTransaction(Transaction Transaction);
}
