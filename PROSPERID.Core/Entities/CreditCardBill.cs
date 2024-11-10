using PROSPERID.Core.Enums;
using PROSPERID.Core.ValueObjects;

namespace PROSPERID.Core.Entities;

public class CreditCardBill : Entity
{
    public DateTime BillDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime BillingPeriod { get; set; }
    public DateTime CloseDate { get; set; }
    public EStatus Status { get; set; }
    public Money TotalAmount { get; set; }
    public Money PaidAmount { get; set; }
    public EPaymentStatus PaymentStatus { get; set; }

    public long? CreditCardId { get; set; }
    public CreditCard CreditCard { get; set; }
    public virtual ICollection<Transaction> Transactions { get; private set; }

    private CreditCardBill()
    {

    }

    public CreditCardBill(long? creditCardId, DateTime billDate, DateTime dueDate,
        decimal totalAmount, decimal paidAmount, EPaymentStatus paymentStatus)
    {
        CreditCardId = creditCardId;
        BillDate = billDate;
        DueDate = dueDate;
        TotalAmount = new(totalAmount);
        PaidAmount = new(paidAmount);
        PaymentStatus = paymentStatus;
        Status = EStatus.Open;
    }

    public void Update(DateTime billDate, DateTime dueDate,
        decimal totalAmount, decimal paidAmount)
    {
        BillDate = billDate;
        DueDate = dueDate;
        TotalAmount = new(totalAmount);
        PaidAmount = new(paidAmount);
        Status = EStatus.Open;
    }

    public bool PayBill(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Valor deve ser positivo!");

        if (PaidAmount + amount > TotalAmount)
            throw new InvalidOperationException("Pagamento excede o total do debíto!");

        PaidAmount += amount;
        UpdateStatus(EPaymentStatus.Paid);
        return true;
    }

    public void UpdateStatus(EPaymentStatus paymentStatus) => PaymentStatus = paymentStatus;

    public bool IsOverdue() => DateTime.Now > DueDate && PaidAmount < TotalAmount;

    public decimal GetRemainingAmount() => TotalAmount - PaidAmount;

    public bool AddTransaction(Transaction transaction)
    {
        if (CreditCard.IsCreditLimitSufficient(transaction.Amount))
        {
            Transactions.Add(transaction);
            return true;
        }
        return false;
    }

    public bool IsPaid() => PaymentStatus.Equals(EPaymentStatus.Paid);

    private void FinalizeBill(CreditCardBill bill)
    {
        bill.Status = EStatus.Close;
        bill.CloseDate = DateTime.Now;
    }

    private void FinalizeBill(DateTime closeDate)
    {
        Status = EStatus.Close;
        CloseDate = DateTime.Now;
    }
}
