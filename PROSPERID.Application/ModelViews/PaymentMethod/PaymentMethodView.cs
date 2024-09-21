using PROSPERID.Application.ModelViews.CreditCard;
using PROSPERID.Core.Enums;

namespace PROSPERID.Application.ModelViews.PaymentMethod;

public class PaymentMethodView
{
    public string Name { get; set; } = string.Empty;
    public long? BankAccountId { get; set; }
    public long? CreditCardId { get; set; }
    public EPaymentMethodType PaymentMethodType { get; set; }
    public bool? IsActive { get; set; }

    public static implicit operator PaymentMethodView(Core.Entities.PaymentMethod paymentMethod)
        => new()
        {
            BankAccountId = paymentMethod.BankAccountId,
            CreditCardId = paymentMethod.CreditCardId,
            PaymentMethodType = paymentMethod.PaymentMethodType,
            IsActive = paymentMethod.IsActive,
        };
}
