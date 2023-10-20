using PROSPERID.Domain.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class UpdateTransactionDTO : TransactionDTO
{
    public UpdateTransactionDTO(Guid id, string description, string category, TransactionType type, decimal amount, DateTime transactionDate, DateTime dueDate)
        : base(description, category, type, amount, transactionDate, dueDate)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
