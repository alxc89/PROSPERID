using PROSPERID.Core.Entities;
using System.Linq.Expressions;

namespace PROSPERID.Core.Interface.Repositories;

public interface ICreditCardBillRepository
{
    Task<CreditCardBill> CreateCreditCardBillAsync(CreditCardBill creditCardBill);
    Task<IEnumerable<CreditCardBill>> GetCreditCardBillsAsync();
    Task<CreditCardBill> GetCreditCardBillByIdAsync(long id);
    Task<CreditCardBill> GetCreditCardBillByIdAsync(long id, params Expression<Func<CreditCardBill, object>>[] includes);
    Task<CreditCardBill> UpdateCreditCardBillAsync(CreditCardBill creditCardBill);
    Task DeleteCreditCardBillAsync(CreditCardBill creditCardBill);
    Task<bool> GetBillByCompetenceMonth(DateTime billingPeriod, long creditCardId);
}
