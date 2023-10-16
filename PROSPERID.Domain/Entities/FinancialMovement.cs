using PROSPERID.Domain.Enums;

namespace PROSPERID.Domain.Entities;

public class FinancialMovement : Entity
{
    public FinancialMovement(string description, string category, TransactionType type, 
        decimal amount, DateTime movementDate, DateTime dueDate)
    {
        Description = description;
        Category = category;
        Type = type;
        Amount = amount;
        MovementDate = movementDate;
        DueDate = dueDate;
        CreatedAt = DateTime.UtcNow;
    }

    public string Description { get; set; }
    public string Category { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime MovementDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

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
        FinancialMovement reverseTransaction = new("Estorno de Pagemento", this.Category, 
            TransactionType.Receipt, reverseAmount, MovementDate, DueDate);
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
        FinancialMovement reverseTransaction = new("Estorno de Recebimento", Category, 
            TransactionType.Receipt, reverseAmount, MovementDate, DueDate);
        account.Transactions.Add(reverseTransaction);
        account.Withdraw(Amount);
        PaymentDate = null;
        return true;
    }
}