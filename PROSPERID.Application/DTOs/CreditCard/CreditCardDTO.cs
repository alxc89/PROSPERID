using PROSPERID.Core.Entities;
using PROSPERID.Core.ValueObjects;

namespace PROSPERID.Application.DTOs.CreditCard;

public class CreditCardDTO
{
    public CardNumber Number { get; set; } = new CardNumber("");
    public string HolderName { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public Money CreditLimit { get; set; } = new Money(0);
    public Money CurrentBalance { get; set; } = new Money(0);
    public DateTime DueDate { get; set; }

    public virtual ICollection<CreditCardBill>? CreditCardBill { get; set; }

    public PaymentMethod? PaymentMethod { get; set; }

    public static implicit operator CreditCardDTO(Core.Entities.CreditCard creditCard)
        => new()
        {
            Number = creditCard.Number,
            HolderName = creditCard.HolderName,
            ExpirationDate = creditCard.ExpirationDate,
            CreditLimit = creditCard.CreditLimit,
            CurrentBalance = creditCard.CurrentBalance,
            DueDate = creditCard.DueDate,
            CreditCardBill = creditCard.CreditCardBill,
            PaymentMethod = creditCard.PaymentMethod
        };
}
