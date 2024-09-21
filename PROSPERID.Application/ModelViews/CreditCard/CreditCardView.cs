using PROSPERID.Application.DTOs.CreditCardBill;
using PROSPERID.Application.ModelViews.PaymentMethod;
using PROSPERID.Core.ValueObjects;

namespace PROSPERID.Application.ModelViews.CreditCard;

public class CreditCardView
{
    public CardNumber Number { get; set; } = new CardNumber("");
    public string HolderName { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public Money CreditLimit { get; set; } = new Money(0);
    public Money CurrentBalance { get; set; } = new Money(0);
    public DateTime DueDate { get; set; }

    public virtual ICollection<CreditCardBillDTO>? CreditCardBillDTO { get; set; }

    public PaymentMethodView? PaymentMethodView { get; set; }

    public static implicit operator CreditCardView(Core.Entities.CreditCard creditCard)
        => new()
        {
            Number = creditCard.Number,
            HolderName = creditCard.HolderName,
            ExpirationDate = creditCard.ExpirationDate,
            CreditLimit = creditCard.CreditLimit,
            CurrentBalance = creditCard.CurrentBalance,
            DueDate = creditCard.DueDate,
            CreditCardBillDTO = [],
            PaymentMethodView = null
        };
}
