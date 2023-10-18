using PROSPERID.Domain.Enums;

namespace PROSPERID.Application.DTOs.FinancialMovement;

public record UpdateFinancialMovementDTO(Guid Id, string Description, string Category, TransactionType Type,
        decimal Amount, DateTime MovementDate, DateTime DueDate);
