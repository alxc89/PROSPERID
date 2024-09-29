using Microsoft.EntityFrameworkCore;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Interface.Repositories;
using PROSPERID.Infra.Context;
using System.Linq.Expressions;

namespace PROSPERID.Infra.Repositories;

public class CreditCardRepository(DataContext context) : ICreditCardRepository
{
    private readonly DataContext _context = context;

    public async Task<CreditCard> CreateCreditCardAsync(CreditCard creditCard)
    {
        try
        {
            await _context
                .CreditCards
                .AddAsync(creditCard);
            await _context.SaveChangesAsync();
            return creditCard;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<CreditCard?> GetCreditCardByIdAsync(long id)
    {
        try
        {
            return await _context
                .CreditCards
                .SingleOrDefaultAsync(b => b.Id == id);
        }
        catch
        {
            throw new Exception("Erro interno!");
        }

    }

    public async Task<CreditCard?> GetCreditCardByIdAsync(long id, params Expression<Func<CreditCard, object>>[] includes)
    {
        try
        {
            var query = _context.CreditCards.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return await query
                //.Include(x => x.CreditCardBills)
                .SingleOrDefaultAsync(c => c.Id == id);
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task DeleteCreditCardAsync(CreditCard creditCard)
    {
        if (creditCard == null) return;
        try
        {
            _context.CreditCards.Remove(creditCard);
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<IEnumerable<CreditCard>> GetCreditCardsAsync()
    {
        try
        {
            return await _context.CreditCards.AsNoTracking().ToListAsync();
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<CreditCard?> UpdateCreditCardAsync(CreditCard creditCard)
    {
        var creditCardUpdated = await _context
             .CreditCards
             .SingleOrDefaultAsync(b => b.Id == creditCard.Id);
        if (creditCardUpdated == null)
            return null;
        try
        {
            _context
                .Entry(creditCardUpdated)
                .CurrentValues
                .SetValues(creditCard);
            await _context.SaveChangesAsync();
            return creditCardUpdated;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<bool> AnyCartCredit(string cartNumber)
    {
        try
        {
            return await _context.CreditCards.AnyAsync(b => b.Number.Value == cartNumber);
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }
}
