using PROSPERID.Core.Entities;

namespace PROSPERID.Core.Interface.Repositories;

public interface IPaymentMethodRepository
{
    Task<PaymentMethod> GetPaymentMethodByIdAsync(long id);
    Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethod paymentMethod);
    Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync();
    Task<PaymentMethod> UpdatePaymentMethodAsync(PaymentMethod paymentMethod);
    Task DeletePaymentMethodAsync(long id);
}
