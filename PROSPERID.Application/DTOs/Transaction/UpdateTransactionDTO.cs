using PROSPERID.Core.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class UpdateTransactionDTO(long id, string description, ETransactionType type,
    decimal amount, DateTime transactionDate, DateTime dueDate, long categoryId, DateTime paymentDate, long? paymentMethodId)
    : TransactionDTO(description, type, amount, transactionDate, dueDate, paymentDate, paymentMethodId, categoryId)
{
    public long Id { get; set; } = id;
}
