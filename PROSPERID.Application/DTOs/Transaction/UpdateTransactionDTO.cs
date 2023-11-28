using PROSPERID.Application.DTOs.Category;
using PROSPERID.Domain.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class UpdateTransactionDTO : TransactionDTO
{
    public UpdateTransactionDTO(Guid id, string description, TransactionType type, decimal amount, DateTime transactionDate, DateTime dueDate, CategoryDTO category)
        : base(description, type, amount, transactionDate, dueDate, category)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}
