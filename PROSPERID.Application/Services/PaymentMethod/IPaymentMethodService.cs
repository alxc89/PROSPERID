using PROSPERID.Application.DTOs.PaymentMethod;
using PROSPERID.Application.ModelViews.PaymentMethod;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.PaymentMethod;

public interface IPaymentMethodService
{
    Task<ServiceResponse<PaymentMethodView>> GetPaymentMethodByIdAsync(long id);
    Task<ServiceResponse<IEnumerable<PaymentMethodView>>> GetAllPaymentMethodAsync();
    Task<ServiceResponse<PaymentMethodView>> CreatePaymentMethodAsync(CreatePaymentMethodDTO createCreditCardBillDTO);
    Task<ServiceResponse<PaymentMethodView>> UpdatePaymentMethodAsync(long id, UpdatePaymentMethodDTO updatePaymentMethodDTO);
    Task<ServiceResponse<PaymentMethodView>> DeletePaymentMethodAsync(long id);
}
