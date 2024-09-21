using PROSPERID.Core.Entities;

namespace PROSPERID.Core.Interface.Repositories;

public interface ICreditCardRepository
{
    Task<CreditCard> CreateCreditCardAsync(CreditCard creditCard);
    Task<IEnumerable<CreditCard>> GetCreditCardsAsync();
    Task<CreditCard?> GetCreditCardByIdAsync(long id);
    Task<CreditCard> UpdateCreditCardAsync(CreditCard creditCard);
    Task DeleteCreditCardAsync(long id);
    Task<bool> AnyCartCredit(string cartNumber);
}
