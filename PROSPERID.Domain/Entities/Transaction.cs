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
        BankAccounts = [];
    }

    protected Transaction()
    {

    }

    public string Description { get; set; } = null!;
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<BankAccount> BankAccounts { get; set; } = null!;

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
        UpdatedAt = DateTime.Now;
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

    public bool CancelPayment(BankAccount account)
    {
        if (Type == TransactionType.Receipt)
            return false;
        decimal reverseAmount = Amount * -1;
        Transaction reverseTransaction = new("Estorno de Pagemento", this.Category,
            TransactionType.Receipt, reverseAmount, TransactionDate, DueDate);
        account.Transactions?.Add(reverseTransaction);
        account.Deposit(reverseAmount);
        PaymentDate = null;
        return true;
    }

    public bool CancelReceipt(BankAccount account)
    {
        if (Type == TransactionType.Payment)
            return false;
        decimal reverseAmount = Amount * -1;
        Transaction reverseTransaction = new("Estorno de Recebimento", Category,
            TransactionType.Receipt, reverseAmount, TransactionDate, DueDate);
        account.Transactions?.Add(reverseTransaction);
        account.Withdraw(Amount);
        PaymentDate = null;
        return true;
    }
}