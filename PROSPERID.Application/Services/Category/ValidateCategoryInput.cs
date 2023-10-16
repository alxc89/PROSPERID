using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.Category;

public static class ValidateCategoryInput<T>
{
    public static ServiceResponse<T> Validate(string name)
    {
        if (string.IsNullOrEmpty(name))
            return new ServiceResponse<T>("Requisição inválida, Nome da Categoria é Obrigatório", 400);
        if (name.Length < 4)
            return new ServiceResponse<T>("Requisição inválida, Categoria deve conter 4 ou mais caracteres", 400);

        return null!;
    }
}
