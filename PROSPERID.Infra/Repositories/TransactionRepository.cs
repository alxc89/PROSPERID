using Microsoft.EntityFrameworkCore;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Interface.Repositories;
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

    public async Task<Transaction?> GetTransactionByIdAsync(Guid id)
    {
        try
        {
            var transaction = _context.Transactions;
            var result = await transaction
                .Include(c => c.Category)
                .SingleOrDefaultAsync(t => t.Id == id);
            return result;
        }
        catch (Exception)
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
    {
        try
        {
            var transactions = await _context
                .Transactions
                .Include(c => c.Category)
                .ToListAsync();
            return transactions;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public Task<Transaction> UpdateTransactionAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}
