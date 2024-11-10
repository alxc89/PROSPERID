using PROSPERID.Application.DTOs.Payment;
using PROSPERID.Application.ModelViews.Transaction;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.Payment;

public interface IPaymentService
{
    Task<ServiceResponse<TransactionView>> ExecutePaymentAsync(long id, PaymentDTO paymentDTO);
}
