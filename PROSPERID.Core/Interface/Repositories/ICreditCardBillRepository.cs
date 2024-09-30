using PROSPERID.Core.Entities;
using System.Linq.Expressions;

namespace PROSPERID.Core.Interface.Repositories;

public interface ICreditCardBillRepository
{
    Task<CreditCardBill> CreateCreditCardBillAsync(CreditCardBill CreditCardBill);
    Task<IEnumerable<CreditCardBill>> GetCreditCardBillsAsync();
    Task<CreditCardBill?> GetCreditCardBillByIdAsync(long id);
    Task<CreditCardBill?> GetCreditCardBillByIdAsync(long id, params Expression<Func<CreditCardBill, object>>[] includes);
    Task<CreditCardBill> UpdateCreditCardBillAsync(CreditCardBill CreditCardBill);
    Task DeleteCreditCardBillAsync(CreditCardBill CreditCardBill);
    Task<bool> AnyCartCredit(string cartNumber);
}
