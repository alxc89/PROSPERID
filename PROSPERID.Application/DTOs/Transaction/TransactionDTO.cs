using PROSPERID.Application.DTOs.Category;
using PROSPERID.Core.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class TransactionDTO
{
    public string Description { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime DueDate { get; set; }
    public CategoryDTO CategoryDTO { get; set; }

    public TransactionDTO(string description, TransactionType type,
        decimal amount, DateTime transactionDate, DateTime dueDate, CategoryDTO categoryDTO)
    {
        Description = description;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDate;
        DueDate = dueDate;
        CategoryDTO = categoryDTO;
    }

    public static implicit operator TransactionDTO(Core.Entities.Transaction transaction)
    {
        return new TransactionDTO(transaction.Description, transaction.Type,
            transaction.Amount, transaction.TransactionDate, transaction.DueDate, transaction.Category);
    }
}