using Microsoft.EntityFrameworkCore;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Interface.Repositories;
using PROSPERID.Infra.Context;

namespace PROSPERID.Infra.Repositories;

public class CreditCardRepository(DataContext context) : ICreditCardRepository
{
    private readonly DataContext _context = context;

    public async Task<CreditCard> CreateCreditCardAsync(CreditCard creditCard)
    {
        try
        {
            await _context
                .CreditCarts
                .AddAsync(creditCard);
            await _context.SaveChangesAsync();
            return creditCard;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task DeleteCreditCardAsync(long id)
    {
        var creditCardDeleted = await _context
            .CreditCarts
            .SingleOrDefaultAsync(b => b.Id == id);
        if (creditCardDeleted == null) return;
        try
        {
            _context.CreditCarts.Remove(creditCardDeleted);
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<CreditCard?> GetCreditCardByIdAsync(long id)
        => await _context.CreditCarts.SingleOrDefaultAsync(b => b.Id == id);

    public async Task<IEnumerable<CreditCard>> GetCreditCardsAsync()
        => await _context.CreditCarts.AsNoTracking().ToListAsync();

    public async Task<CreditCard?> UpdateCreditCardAsync(CreditCard creditCard)
    {
        var creditCardUpdated = await _context
             .CreditCarts
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
        => await _context.CreditCarts.AnyAsync(b => b.Number.Value == cartNumber);
}
