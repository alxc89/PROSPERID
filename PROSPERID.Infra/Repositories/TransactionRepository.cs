using Microsoft.EntityFrameworkCore;
using PROSPERID.Domain.Entities;
using PROSPERID.Domain.Interface.Repositories;
using PROSPERID.Infra.Context;

namespace PROSPERID.Infra.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly DataContext _context;
    public TransactionRepository(DataContext context)
        => _context = context;
    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        try
        {
            await _context
                .Transactions
                .AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public Task<Transaction> DeleteTransactionAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsTransaction(Transaction transaction)
    {
        try
        {
            return await _context
                .Transactions
                .AnyAsync(t => t.Description == transaction.Description &&
                          t.Amount == transaction.Amount &&
                          t.CategoryId == transaction.CategoryId);
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public Task<Transaction> GetTransactionByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> GetTransactionsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> UpdateTransactionAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}
