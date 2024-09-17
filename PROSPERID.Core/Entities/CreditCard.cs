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

    public virtual ICollection<CreditCardBill> CreditCardBill { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    private CreditCard()
    {

    }

    public CreditCard(CardNumber number, string holderName, DateTime expirationDate,
        Money creditLimit, DateTime dueDate)
    {
        Number = number;
        HolderName = holderName;
        ExpirationDate = expirationDate;
        CreditLimit = creditLimit;
        CurrentBalance = new Money(0);
        DueDate = dueDate;
        CreditCardBill = [];
    }

    public void AddBalance(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("É necessário que o valor seja positivo!");

        CurrentBalance = new Money(amount);
    }

    public void AddBill(CreditCardBill bill) => CreditCardBill.Add(bill);

    public bool IsExpired() => DateTime.Now > ExpirationDate;

    public decimal GetAvailableBalance() => (CreditLimit - CurrentBalance);
}
