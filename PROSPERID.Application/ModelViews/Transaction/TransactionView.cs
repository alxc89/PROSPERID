using PROSPERID.Application.DTOs.Transaction;
using Entities = PROSPERID.Core.Entities;
using PROSPERID.Core.Enums;

namespace PROSPERID.Application.ModelViews.Transaction;
public class TransactionView
{
    public long Id { get; set; }
    public string Description { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime DueDate { get; set; }
    public long CategoryId { get; set; }

    public TransactionView(long id, string description, long categoryId, TransactionType type, decimal amount, DateTime transactionDate, DateTime dueDate)
    {
        Id = id;
        Description = description;
        CategoryId = categoryId;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDate;
        DueDate = dueDate;
    }

    public TransactionView(string description, long categoryId, TransactionType type, decimal amount, DateTime transactionDate, DateTime dueDate)
    {
        Description = description;
        CategoryId = categoryId;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDate;
        DueDate = dueDate;
    }

    public static implicit operator TransactionView(UpdateTransactionDTO transaction)
    {
        return new TransactionView(transaction.Id, transaction.Description, transaction.CategoryId, transaction.Type,
            transaction.Amount, transaction.TransactionDate, transaction.DueDate);
    }

    public static implicit operator TransactionView(Entities.Transaction transaction)
    {
        return new TransactionView(transaction.Id, transaction.Description, transaction.CategoryId, transaction.Type,
            transaction.Amount, transaction.TransactionDate, transaction.DueDate);
    }

    //public static implicit operator TransactionView(CreateTransactionDTO transaction)
    //{
    //    CategoryDTO categoryDTO = new(transaction.Category.Id, transaction.Category.Name);
    //    return new TransactionView(transaction.Description, categoryDTO, transaction.Type,
    //        transaction.Amount, transaction.TransactionDate, transaction.DueDate);
    //}

    public static implicit operator TransactionView(TransactionDTO transaction)
    {
        return new TransactionView(transaction.Description, transaction.CategoryId, transaction.Type,
            transaction.Amount, transaction.TransactionDate, transaction.DueDate);
    }
}