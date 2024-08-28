using PROSPERID.Application.DTOs.Category;
using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Enums;

namespace PROSPERID.Application.ModelViews;
public class TransactionView
{
    public TransactionView(Guid id, string description, CategoryDTO category, TransactionType type, decimal amount, DateTime transactionDate, DateTime dueDate)
    {
        Id = id;
        Description = description;
        Category = category;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDate;
        DueDate = dueDate;
    }

    public TransactionView(string description, CategoryDTO category, TransactionType type, decimal amount, DateTime transactionDate, DateTime dueDate)
    {
        Description = description;
        Category = category;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDate;
        DueDate = dueDate;
    }

    public Guid Id { get; set; }
    public string Description { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime DueDate { get; set; }
    public CategoryDTO Category { get; set; }

    public static implicit operator TransactionView(UpdateTransactionDTO transaction)
    {
        return new TransactionView(transaction.Id, transaction.Description, transaction.CategoryDTO, transaction.Type,
            transaction.Amount, transaction.TransactionDate, transaction.DueDate);
    }

    public static implicit operator TransactionView(Transaction transaction)
    {
        return new TransactionView(transaction.Id, transaction.Description, transaction.Category, transaction.Type,
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
        return new TransactionView(transaction.Description, transaction.CategoryDTO, transaction.Type,
            transaction.Amount, transaction.TransactionDate, transaction.DueDate);
    }
}