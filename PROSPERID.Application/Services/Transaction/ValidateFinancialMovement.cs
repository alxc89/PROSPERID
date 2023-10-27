using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.Transaction;

public static class ValidateTransaction<T>
{
    public static ServiceResponse<T> Validate(TransactionDTO input)
    {
        if (string.IsNullOrEmpty(input.Description))
            return new ServiceResponse<T>("Requisição inválida, Descrição é obrigatória!", 400);
        if (string.IsNullOrEmpty(input.Type.ToString()))
            return new ServiceResponse<T>("Requisição inválida, Tipo é obrigatório!", 400);
        if (input?.Amount is null)
            return new ServiceResponse<T>("Requisição inválida, Valor é obrigatório!", 400);
        if (input?.TransactionDate == null)
            return new ServiceResponse<T>("Requisição inválida, Data da Transação é obrigatório!", 400);
        if (input?.DueDate == null)
            return new ServiceResponse<T>("Requisição inválida, Data de Vencimento é obrigatório!", 400);
        return null!;
    }
}