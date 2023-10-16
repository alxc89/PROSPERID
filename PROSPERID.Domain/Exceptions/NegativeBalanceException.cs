namespace PROSPERID.Domain.Exceptions;

public class NegativeBalanceException : Exception
{
    public NegativeBalanceException() :
        base("Não é possível informar o Saldo menor que 0!")
    { }
}
