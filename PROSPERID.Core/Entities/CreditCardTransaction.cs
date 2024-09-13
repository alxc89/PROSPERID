namespace PROSPERID.Core.Entities;

public class CreditCardTransaction
{
    public int CreditCardBillId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }

    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    private CreditCardTransaction() { }

    public CreditCardTransaction(int billId, decimal amount, DateTime transactionDate, string description)
    {
        CreditCardBillId = billId;
        Amount = amount;
        TransactionDate = transactionDate;
        Description = description;
    }
}
