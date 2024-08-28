namespace PROSPERID.Core.Exceptions;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException() :
        base("Não foi possível realizar a retirada, pois o valor a ser retirado é maior que o valor em depósito!")
    { }
}
