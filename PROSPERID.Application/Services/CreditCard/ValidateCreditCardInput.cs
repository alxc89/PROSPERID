using PROSPERID.Application.DTOs.CreditCard;
using PROSPERID.Application.Services.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROSPERID.Application.Services.CreditCard;

public static class ValidateCreditCardInput<T>
{
    public static ServiceResponse<T> Validate(string number, string holderName, DateTime? expirationDate,
        decimal creditLimit, DateTime? dueDate)
    {
        if (string.IsNullOrEmpty(number))
            return new ServiceResponse<T>("Requisição inválida, Número do Cartão é Obrigatório", 400);
        if (string.IsNullOrEmpty(holderName))
            return new ServiceResponse<T>("Requisição inválida, Proprietário do Cartão é Obrigatório", 400);
        if (expirationDate == null)
            return new ServiceResponse<T>("Requisição inválida, Data de Expiração do Cartão é Obrigatório", 400);
        if (creditLimit <= 0)
            return new ServiceResponse<T>("Requisição inválida, Limite de crédito do Cartão menor ou igual a 0", 400);
        if (dueDate == null)
            return new ServiceResponse<T>("Requisição inválida, Data de Vencimento do Cartão é Obrigatório", 400);

        return null!;
    }
}
