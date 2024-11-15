using PROSPERID.Core.Enums;
using PROSPERID.Core.ValueObjects;

namespace PROSPERID.Core.Entities;

public class Transaction : Entity
{
    public string Description { get; set; } = null!;
    public ETransactionType Type { get; set; }
    public Money Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public PaymentMethod PaymentMethod { get; set; } = null;
    public BankAccount BankAccount { get; set; } = null;
    public CreditCardBill CreditCardBill { get; set; } = null;
    public long? BankAccountId { get; set; } //Ver se será setado pela forma de pagamento
    public long? CreditCardBillId { get; set; } //Ver se será setado pela forma de pagamento
    public long? PaymentMethodId { get; set; }


    public Transaction(string description, long categoryId, ETransactionType type,
        decimal amount, DateTime transactionDateDate, DateTime dueDate)
    {
        Description = description;
        CategoryId = categoryId;
        Type = type;
        Amount = new Money(amount);
        TransactionDate = transactionDateDate;
        DueDate = dueDate;
    }

    protected Transaction()
    {

    }

    public void Update(string description, long idCategory, ETransactionType type,
        decimal amount, DateTime transactionDateDate, DateTime dueDate)
    {
        //if (PaymentDate.HasValue)
        //    throw new Exception("Transação paga, não é possível alterar!");
        Description = description;
        CategoryId = idCategory;
        Type = type;
        Amount = new Money(amount);
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

    public bool ExecutePayment(CreditCardBill creditCardBill, DateTime datePayment)
    {
        if (creditCardBill.AddTransaction(this))
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
        if (Type == ETransactionType.Receipt)
            return false;
        decimal reverseAmount = Amount * -1;
        Transaction reverseTransaction = new("Estorno de Pagemento", this.Category.Id,
            ETransactionType.Receipt, reverseAmount, TransactionDate, DueDate);
        account.Transactions?.Add(reverseTransaction);
        account.Deposit(reverseAmount);
        PaymentDate = null;
        return true;
    }

    public bool CancelReceipt(BankAccount account)
    {
        if (Type == ETransactionType.Payment)
            return false;
        decimal reverseAmount = Amount * -1;
        Transaction reverseTransaction = new("Estorno de Recebimento", Category.Id,
            ETransactionType.Receipt, reverseAmount, TransactionDate, DueDate);
        account.Transactions?.Add(reverseTransaction);
        account.Withdraw(Amount);
        PaymentDate = null;
        return true;
    }
}