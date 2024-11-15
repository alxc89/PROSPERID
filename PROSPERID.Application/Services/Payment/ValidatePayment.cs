using PROSPERID.Application.DTOs.Payment;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.Payment;

public static class ValidatePayment<T>
{
    public static ServiceResponse<T> Validate(PaymentDTO input)
    {
        if (input?.PaymentMethodId == null || input.PaymentMethodId == 0)
            return new ServiceResponse<T>("Requisição inválida, Forma de Pagamento é obrigatória!", 400);
        if (string.IsNullOrEmpty(input.PaymentMethodId.ToString()))
            return new ServiceResponse<T>("Requisição inválida, Data de Pagamento é obrigatório!", 400);
        return null!;
    }
}
