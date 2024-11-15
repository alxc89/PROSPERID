using PROSPERID.Core.Enums;

namespace PROSPERID.Core.Entities;

public class PaymentMethod : Entity
{
    public string Name { get; set; }
    public long? BankAccountId { get => _bankAccountId; private set => _bankAccountId = value; }
    public long? CreditCardId { get => _creditCardId; private set => _creditCardId = value; }
    public EPaymentMethodType PaymentMethodType { get; set; }
    public bool? IsActive { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = [];

    public BankAccount BankAccount { get; set; }
    public CreditCard CreditCard { get; set; }

    private long? _bankAccountId;
    private long? _creditCardId;

    private PaymentMethod()
    {

    }

    public PaymentMethod(string name, EPaymentMethodType paymentMethodType, long bankAccountId = 0, long creditCardId = 0)
    {
        Name = name;
        PaymentMethodType = paymentMethodType;
        _bankAccountId = bankAccountId == 0 ? null : bankAccountId;
        _creditCardId = creditCardId == 0 ? null : creditCardId;
        ValidatePaymentMethodType();
    }

    public void Update(string name)
    {
        Name = name;
    }

    public void SetBankAccount(int? bankAccountId)
    {
        if (PaymentMethodType != EPaymentMethodType.BankAccount)
            throw new InvalidOperationException("Conta bancária só pode ser definida para métodos de pagamento do tipo BankAccount.");

        _bankAccountId = bankAccountId;
        _creditCardId = null;
    }

    public void SetCreditCard(int? creditCardId)
    {
        if (PaymentMethodType != EPaymentMethodType.CreditCard)
            throw new InvalidOperationException("Cartão de crédito só pode ser definido para métodos de pagamento do tipo CreditCard.");

        _creditCardId = creditCardId;
        _bankAccountId = null;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    private void ValidatePaymentMethodType()
    {
        switch (PaymentMethodType)
        {
            case EPaymentMethodType.BankAccount:
            case EPaymentMethodType.CreditCard:
            case EPaymentMethodType.Other:
                break;
            default:
                throw new InvalidOperationException("Tipo de pagamento inválido.");
        }
    }

    public bool IsValid()
    {
        return PaymentMethodType switch
        {
            EPaymentMethodType.BankAccount => _bankAccountId.HasValue && !_creditCardId.HasValue,
            EPaymentMethodType.CreditCard => _creditCardId.HasValue && !_bankAccountId.HasValue,
            EPaymentMethodType.Other => !_bankAccountId.HasValue && !_creditCardId.HasValue,
            _ => false,
        };
    }
}
