using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Core.Enums;
using PROSPERID.Core.ValueObjects;

namespace PROSPERID.Application.DTOs.CreditCardBill;

public class CreditCardBillDTO
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
    //public CreditCardDTO CreditCard { get; set; }
    public virtual ICollection<TransactionDTO>? Transactions { get; set; }

    public static implicit operator CreditCardBillDTO(Core.Entities.CreditCardBill creditCardBill)
        => new()
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
            //Transactions = creditCardBill.Transactions
        };
}