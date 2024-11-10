using PROSPERID.Core.Entities;

namespace PROSPERID.Core.Interface.Repositories;

public interface IPaymentMethodRepository
{
    Task<PaymentMethod> GetPaymentMethodByIdAsync(long id);

}
