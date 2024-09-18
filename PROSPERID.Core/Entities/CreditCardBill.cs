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
    public virtual ICollection<Transaction> Transactions { get; set; }

    private CreditCardBill()
    {

    }

    public CreditCardBill(int creditCardId, DateTime billDate, DateTime dueDate,
        Money totalAmount, Money paidAmount, EPaymentStatus paymentStatus)
    {
        CreditCardId = creditCardId;
        BillDate = billDate;
        DueDate = dueDate;
        TotalAmount = totalAmount;
        PaidAmount = paidAmount;
        PaymentStatus = paymentStatus;
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

    private void AddTransaction(Transaction transaction) => Transactions.Add(transaction);
}
