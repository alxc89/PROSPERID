using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.BankAccount;

public static class ValidateBankAccountInput<T>
{
    public static ServiceResponse<T> Validate(string accountNumber, string accountHolder, decimal balance)
    {
        if (string.IsNullOrEmpty(accountNumber))
            return new ServiceResponse<T>("Requisição inválida, Número da Conta é Obrigatório", 400);
        if (string.IsNullOrEmpty(accountHolder))
            return new ServiceResponse<T>("Requisição inválida, Proprietário da Conta é Obrigatório", 400);
        if (accountNumber.Length < 3)
            return new ServiceResponse<T>("Requisição inválida, Número da conta deve conter 3 ou mais caracteres", 400);
        if (accountHolder.Length < 3)
            return new ServiceResponse<T>("Requisição inválida, Nome do Proprietário deve conter 3 ou mais caracteres", 400);
        if (balance < 0)
            return new ServiceResponse<T>("Requisição inválida, Saldo da conta não pode ser negativa", 400);

        return null!;
    }
}
