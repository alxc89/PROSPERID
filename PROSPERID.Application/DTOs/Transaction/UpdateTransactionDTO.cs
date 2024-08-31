using PROSPERID.Application.DTOs.Category;
using PROSPERID.Core.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class UpdateTransactionDTO(long id, string description, TransactionType type, 
    decimal amount, DateTime transactionDate, DateTime dueDate, CategoryDTO category) : TransactionDTO(description, type, amount, transactionDate, dueDate, category)
{
    public long Id { get; set; } = id;
}
