using PROSPERID.Application.ModelViews.CreditCardBill;

namespace PROSPERID.Application.DTOs.CreditCardBill;

public class CreateCreditCardBillDTO : CreditCardBillDTO
{
    public static implicit operator CreateCreditCardBillDTO(CreditCardBillView creditCardBillView)
    {
        return new CreateCreditCardBillDTO { 
            BillDate = creditCardBillView.BillDate,
            BillingPeriod = creditCardBillView.BillingPeriod,
            CloseDate = creditCardBillView.CloseDate,
            CreditCardId = creditCardBillView.CreditCardId,
            DueDate = creditCardBillView.DueDate,
            PaidAmount = creditCardBillView.PaidAmount,
            PaymentStatus = creditCardBillView.PaymentStatus,
            Status = creditCardBillView.Status,
            TotalAmount = creditCardBillView.TotalAmount,
            Transactions = [],
            
            //Transactions = creditCardBillView.Transactions
        };
    }
}
