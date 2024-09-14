using PROSPERID.Core.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class UpdateTransactionDTO(long id, string description, ETransactionType type,
    decimal amount, DateTime transactionDate, DateTime dueDate, long categoryId)
    : TransactionDTO(description, type, amount, transactionDate, dueDate, categoryId)
{
    public long Id { get; set; } = id;
}
