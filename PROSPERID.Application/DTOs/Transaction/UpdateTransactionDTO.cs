using PROSPERID.Domain.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public record UpdateTransactionDTO(Guid Id, string Description, string Category, TransactionType Type,
        decimal Amount, DateTime TransactionDate, DateTime DueDate);
