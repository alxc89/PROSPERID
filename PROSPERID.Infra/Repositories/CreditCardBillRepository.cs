using Microsoft.EntityFrameworkCore;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Interface.Repositories;
using PROSPERID.Infra.Context;
using System.Linq.Expressions;

namespace PROSPERID.Infra.Repositories;

public class CreditCardBillRepository(DataContext context) : ICreditCardBillRepository
{
    private readonly DataContext _context = context;

    public async Task<CreditCardBill> CreateCreditCardBillAsync(CreditCardBill CreditCardBill)
    {
        try
        {
            await _context
                .CreditCardBills
                .AddAsync(CreditCardBill);
            await _context.SaveChangesAsync();
            return CreditCardBill;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<CreditCardBill?> GetCreditCardBillByIdAsync(long id)
    {
        try
        {
            return await _context
                .CreditCardBills
                .SingleOrDefaultAsync(b => b.Id == id);
        }
        catch
        {
            throw new Exception("Erro interno!");
        }

    }

    public async Task<CreditCardBill?> GetCreditCardBillByIdAsync(long id, params Expression<Func<CreditCardBill, object>>[] includes)
    {
        try
        {
            var query = _context.CreditCardBills.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return await query
                .SingleOrDefaultAsync(c => c.Id == id);
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task DeleteCreditCardBillAsync(CreditCardBill CreditCardBill)
    {
        if (CreditCardBill == null) return;
        try
        {
            _context.CreditCardBills.Remove(CreditCardBill);
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<IEnumerable<CreditCardBill>> GetCreditCardBillsAsync()
    {
        try
        {
            return await _context.CreditCardBills.AsNoTracking().ToListAsync();
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<CreditCardBill?> UpdateCreditCardBillAsync(CreditCardBill CreditCardBill)
    {
        var CreditCardBillUpdated = await _context
             .CreditCardBills
             .SingleOrDefaultAsync(b => b.Id == CreditCardBill.Id);
        if (CreditCardBillUpdated == null)
            return null;
        try
        {
            _context
                .Entry(CreditCardBillUpdated)
                .CurrentValues
                .SetValues(CreditCardBill);
            await _context.SaveChangesAsync();
            return CreditCardBillUpdated;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }
}
