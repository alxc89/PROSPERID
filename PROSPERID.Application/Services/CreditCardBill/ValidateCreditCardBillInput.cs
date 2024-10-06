using PROSPERID.Application.DTOs.CreditCardBill;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.CreditCardBill;

public class ValidateCreditCardBillInput<T>
{
    public static ServiceResponse<T> Validate(CreditCardBillDTO input)
    {
        if (input.CreditCardId == 0)
            return new ServiceResponse<T>("Requisição inválida, Não foi informado a qual cartão se refere a essa Fatura!", 400);
        if (input?.CloseDate == null)
            return new ServiceResponse<T>("Requisição inválida, Não foi informado a Data de Fechamento para a Fatura!", 400);
        if (input?.BillingPeriod == null)
            return new ServiceResponse<T>("Requisição inválida, Não foi informado a Data de Competência para a Fatura!", 400);
        if (input?.DueDate == null)
            return new ServiceResponse<T>("Requisição inválida, Não foi informado a Data de Vencimento para a Fatura!", 400);
        if (input.Transactions != null && input.Transactions.Any(t => t.CategoryId == 0 || t?.CategoryId == null))
            return new ServiceResponse<T>("Requisição inválida, Existe transação que não foi informado a Categoria!", 400);
        return null!;
    }
}
