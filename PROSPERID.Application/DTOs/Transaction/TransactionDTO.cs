using PROSPERID.Domain.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class TransactionDTO
{
    public TransactionDTO(string description, string category, TransactionType type,
        decimal amount, DateTime transactionDate, DateTime dueDate)
    {
        Description = description;
        Category = category;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDate;
        DueDate = dueDate;
    }

    public string Description { get; set; }
    public string Category { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime DueDate { get; set; }

    public static implicit operator TransactionDTO(Domain.Entities.Transaction transaction)
    {
        return new TransactionDTO(transaction.Description, transaction.Category, transaction.Type,
            transaction.Amount, transaction.TransactionDate, transaction.DueDate);
    }
}