using PROSPERID.Core.Enums;

namespace PROSPERID.Application.DTOs.PaymentMethod;

public class PaymentMethodDTO
{
    public string Name { get; set; } = string.Empty;
    public long BankAccountId { get; set; } = 0;
    public long CreditCardId { get; set; } = 0;
    public EPaymentMethodType PaymentMethodType { get; set; }
    public bool? IsActive { get; set; }
}
