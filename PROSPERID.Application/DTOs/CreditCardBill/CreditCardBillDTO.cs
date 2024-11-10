using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Core.Enums;
using PROSPERID.Core.ValueObjects;

namespace PROSPERID.Application.DTOs.CreditCardBill;

public class CreditCardBillDTO
{
    private DateTime _billDate;
    private DateTime _dueDate;
    private DateTime _billingPeriod;
    private DateTime _closeDate;

    public DateTime BillDate { get => _billDate.Date; set => _billDate = value; }
    public DateTime DueDate { get => _dueDate.Date; set => _dueDate = value; }
    public DateTime BillingPeriod { get => _billingPeriod.Date; set => _billingPeriod = value; }
    public DateTime CloseDate { get => _closeDate.Date; set => _closeDate = value; }
    public EStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
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