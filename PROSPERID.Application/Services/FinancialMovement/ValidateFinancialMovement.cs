using PROSPERID.Application.DTOs.FinancialMovement;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.FinancialMovement;

public static class ValidateFinancialMovement<T>
{
    public static ServiceResponse<T> Validate(FinancialMovementDTO input)
    {
        if (string.IsNullOrEmpty(input.Description))
            return new ServiceResponse<T>("Requisição inválida, Descrição é obrigatória!", 400);
        if (string.IsNullOrEmpty(input.Type.ToString()))
            return new ServiceResponse<T>("Requisição inválida, Tipo é obrigatório!", 400);
        if (string.IsNullOrEmpty(input.Amount.ToString()))
            return new ServiceResponse<T>("Requisição inválida, Valor é obrigatório!", 400);
        if (input?.MovementDate == null)
            return new ServiceResponse<T>("Requisição inválida, Data do Movimento é obrigatório!", 400);
        if (input?.DueDate == null)
            return new ServiceResponse<T>("Requisição inválida, Data de Vencimento é obrigatório!", 400);
        return null!;
    }
}