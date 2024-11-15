using Microsoft.IdentityModel.Tokens;
using PROSPERID.Application.DTOs.PaymentMethod;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Core.Enums;

namespace PROSPERID.Application.Services.PaymentMethod;
public static class ValidatePaymentMethodInput<T>
{
    public static ServiceResponse<T> Validate(PaymentMethodDTO input)
    {
        if (input.Name.IsNullOrEmpty())
            return new ServiceResponse<T>("Requisição inválida, Nome do método de pagamento é obrigatório!", 400);
        if (input.PaymentMethodType == EPaymentMethodType.CreditCard && (input.CreditCardId == null || input.CreditCardId == 0))
            return new ServiceResponse<T>("Requisição inválida, Tipo de pagamento é cartão de crédito, mas não foi informado um cartão de crédito, verifique!", 400);
        if (input.PaymentMethodType == EPaymentMethodType.BankAccount && (input.BankAccountId == null || input.BankAccountId == 0))
            return new ServiceResponse<T>("Requisição inválida, Tipo de pagamento é conta bancária, mas não foi informado uma conta bancária, verifique!", 400);
        if (!Enum.IsDefined(input.PaymentMethodType))
            return new ServiceResponse<T>("Requisição inválida, Tipo de pagamento inválido!", 400);

        return null!;
    }
}