using PROSPERID.Core.Entities;
using System.Linq.Expressions;

namespace PROSPERID.Core.Interface.Repositories;

public interface ICreditCardRepository
{
    Task<CreditCard> CreateCreditCardAsync(CreditCard creditCard);
    Task<IEnumerable<CreditCard>> GetCreditCardsAsync();
    Task<CreditCard?> GetCreditCardByIdAsync(long id);
    Task<CreditCard?> GetCreditCardByIdAsync(long id, params Expression<Func<CreditCard, object>>[] includes);
    Task<CreditCard> UpdateCreditCardAsync(CreditCard creditCard);
    Task DeleteCreditCardAsync(CreditCard creditCard);
    Task<bool> AnyCartCredit(string cartNumber);
}
