using PROSPERID.Domain.Enums;

namespace PROSPERID.Domain.Entities;

public class Transaction : Entity
{
    public Transaction(string description, Category category, TransactionType type,
        decimal amount, DateTime transactionDateDate, DateTime dueDate)
    {
        Description = description;
        Category = category;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDateDate;
        DueDate = dueDate;
        CreatedAt = DateTime.UtcNow;
    }

    public Transaction()
    {
        
    }

    public string Description { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public void Update(string description, Guid idCategory, TransactionType type,
        decimal amount, DateTime transactionDateDate, DateTime dueDate)
    {
        //if (PaymentDate.HasValue)
        //    throw new Exception("Transação paga, não é possível alterar!");
        Description = description;
        CategoryId = idCategory;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDateDate;
        DueDate = dueDate;
    }

    public bool ExecutePayment(BankAccount account, DateTime datePayment)
    {
        if (account.MakePayment(Amount * -1))
        {
            PaymentDate = datePayment;
            return true;
        }
        else
            return false;
    }

    public bool ExecuteReceipt(BankAccount account, DateTime datePayment)
    {
        account.Deposit(Amount);
        PaymentDate = datePayment;
        return true;
    }

    public bool ExecutePaymentCancellation(BankAccount account)
    {
        if (Type == TransactionType.Receipt)
            return false;
        decimal reverseAmount = Amount * -1;
        Transaction reverseTransaction = new("Estorno de Pagemento", this.Category,
            TransactionType.Receipt, reverseAmount, TransactionDate, DueDate);
        account.Transactions.Add(reverseTransaction);
        account.Deposit(reverseAmount);
        PaymentDate = null;
        return true;
    }

    public bool ExecuteReceiptCancellation(BankAccount account)
    {
        if (Type == TransactionType.Payment)
            return false;
        decimal reverseAmount = Amount * -1;
        Transaction reverseTransaction = new("Estorno de Recebimento", Category,
            TransactionType.Receipt, reverseAmount, TransactionDate, DueDate);
        account.Transactions.Add(reverseTransaction);
        account.Withdraw(Amount);
        PaymentDate = null;
        return true;
    }
}