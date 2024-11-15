using PROSPERID.Core.ValueObjects;

namespace PROSPERID.Core.Entities;

public class CreditCard : Entity
{
    public CardNumber Number { get; set; }
    public string HolderName { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Money CreditLimit { get; set; }
    public Money CurrentBalance { get; set; }
    public DateTime DueDate { get; set; }

    public ICollection<CreditCardBill> CreditCardBills { get; set; }

    public ICollection<PaymentMethod> PaymentMethods { get; set; } = [];

    private CreditCard()
    {

    }

    public CreditCard(string number, string holderName, DateTime expirationDate,
        Money creditLimit, DateTime dueDate)
    {
        Number = new CardNumber(number);
        HolderName = holderName;
        ExpirationDate = expirationDate;
        CreditLimit = creditLimit;
        CurrentBalance = new Money(0);
        DueDate = dueDate;
        CreditCardBills = [];
    }

    public void Update(string number, string holderName, DateTime expirationDate,
        decimal creditLimit, decimal currentBalance, DateTime dueDate)
    {
        Number = new CardNumber(number);
        HolderName = holderName;
        ExpirationDate = expirationDate;
        CreditLimit = new Money(creditLimit);
        CurrentBalance = new Money(currentBalance);
        DueDate = dueDate;
    }

    public void AddBalance(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("É necessário que o valor seja positivo!");

        CurrentBalance = new Money(amount);
    }

    public void AddBill(CreditCardBill bill) => CreditCardBills.Add(bill);

    public bool IsExpired() => DateTime.Now > ExpirationDate;

    public decimal GetAvailableBalance() => (CreditLimit - CurrentBalance);

    public bool IsCreditLimitSufficient(Money amount) => (CurrentBalance + amount) <= CreditLimit;

    public bool HasCreditCardBill()
        => CreditCardBills.Count > 0;

    public bool HasLinkedPaymentMethod()
        => PaymentMethods.Any();
}
