using PROSPERID.Core.ValueObjects;

namespace PROSPERID.Application.DTOs.CreditCard;

public class CreateCreditCardDTO
{
    public CardNumber Number { get; set; } = new CardNumber("");
    public string HolderName { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public Money CreditLimit { get; set; } = new Money(0);
    public Money CurrentBalance { get; set; } = new Money(0);
    public DateTime DueDate { get; set; }
}
