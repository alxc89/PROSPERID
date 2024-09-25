using Microsoft.AspNetCore.Components.Forms;
using PROSPERID.Application.DTOs.CreditCard;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.CreditCard;

public static class ValidateCreditCardInput<T>
{
    public static ServiceResponse<T> Validate(CreditCardDTO input)
    {
        if (string.IsNullOrEmpty(input.Number))
            return new ServiceResponse<T>("Requisição inválida, Número do Cartão é Obrigatório", 400);
        if (string.IsNullOrEmpty(input.HolderName))
            return new ServiceResponse<T>("Requisição inválida, Proprietário do Cartão é Obrigatório", 400);
        if (input?.ExpirationDate == null)
            return new ServiceResponse<T>("Requisição inválida, Data de Expiração do Cartão é Obrigatório", 400);
        if (input.CreditLimit <= 0)
            return new ServiceResponse<T>("Requisição inválida, Limite de crédito do Cartão menor ou igual a 0", 400);
        if (input?.DueDate == null)
            return new ServiceResponse<T>("Requisição inválida, Data de Vencimento do Cartão é Obrigatório", 400);

        return null!;
    }
}
