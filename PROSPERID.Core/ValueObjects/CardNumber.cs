namespace PROSPERID.Core.ValueObjects;

public record CardNumber
{
    public string Value { get; }

    public CardNumber(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Número de cartão inválido");
        Value = value;
    }

    private bool IsValid(string number)
        => number.Length > 16 && number.All(char.IsDigit);
}
