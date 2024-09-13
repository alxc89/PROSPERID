namespace PROSPERID.Core.ValueObjects;

public record Money
{
    public decimal Amount { get; }

    public Money(decimal amount)
    {
        Amount = amount;
    }

    public static Money operator +(Money a, Money b) =>
        new(a.Amount + b.Amount);

    public static Money operator +(Money a, decimal b) =>
        new(a.Amount + b);

    public static Money operator -(Money a, Money b) =>
        new(a.Amount - b.Amount);

    public static implicit operator decimal(Money a) => a.Amount;
}