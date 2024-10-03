using PROSPERID.Application.ModelViews.Transaction;
using PROSPERID.Core.Enums;
using PROSPERID.Core.ValueObjects;

namespace PROSPERID.Application.ModelViews.CreditCardBill;

public class CreditCardBillView
{
    public DateTime BillDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime BillingPeriod { get; set; }
    public DateTime CloseDate { get; set; }
    public EStatus Status { get; set; }
    public Money TotalAmount { get; set; } = new Money(0);
    public Money PaidAmount { get; set; } = new Money(0);
    public EPaymentStatus PaymentStatus { get; set; }

    public long? CreditCardId { get; set; }
    public virtual ICollection<TransactionView>? Transactions { get; set; }

    public static implicit operator CreditCardBillView(Core.Entities.CreditCardBill creditCardBill)
    {
        ICollection<TransactionView> _transactionView = [];

        if (creditCardBill.Transactions != null)
            foreach (var bill in creditCardBill.Transactions)
                _transactionView.Add(bill);

        return new()
        {
            BillDate = creditCardBill.BillDate,
            DueDate = creditCardBill.DueDate,
            BillingPeriod = creditCardBill.BillingPeriod,
            CloseDate = creditCardBill.CloseDate,
            Status = creditCardBill.Status,
            TotalAmount = creditCardBill.TotalAmount,
            PaidAmount = creditCardBill.PaidAmount,
            PaymentStatus = creditCardBill.PaymentStatus,
            CreditCardId = creditCardBill.CreditCardId,
            Transactions = _transactionView
        };
    }
}
