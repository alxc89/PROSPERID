using PROSPERID.Application.DTOs.CreditCardBill;
using PROSPERID.Application.ModelViews.PaymentMethod;

namespace PROSPERID.Application.ModelViews.CreditCard;

public class CreditCardView
{
    private DateTime _dueDate;
    private DateTime _expirationDate;

    public long Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string HolderName { get; set; } = string.Empty;
    public DateTime ExpirationDate { get => _expirationDate.Date; set => _expirationDate = value; }
    public decimal CreditLimit { get; set; }
    public decimal CurrentBalance { get; set; }
    public DateTime DueDate { get => _dueDate.Date; set => _dueDate = value; }

    public virtual ICollection<CreditCardBillDTO>? CreditCardBillDTO { get; set; }

    public PaymentMethodView? PaymentMethodView { get; set; }

    public static implicit operator CreditCardView(Core.Entities.CreditCard creditCard)
    {
        ICollection<CreditCardBillDTO> _creditCardBillDTO = [];

        if (creditCard.CreditCardBill != null)
            foreach (var bill in creditCard.CreditCardBill)
                _creditCardBillDTO.Add(bill);

        return new()
        {
            Id = creditCard.Id,
            Number = creditCard.Number.Value,
            HolderName = creditCard.HolderName,
            ExpirationDate = creditCard.ExpirationDate.Date,
            CreditLimit = creditCard.CreditLimit,
            CurrentBalance = creditCard.CurrentBalance,
            DueDate = creditCard.DueDate.Date,
            CreditCardBillDTO = _creditCardBillDTO,
            PaymentMethodView = null
        };
    }
}
