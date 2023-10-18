using PROSPERID.Domain.Enums;

namespace PROSPERID.Application.DTOs.FinancialMovement;

public class CreateFinancialMovementDTO : FinancialMovementDTO
{
    public CreateFinancialMovementDTO(string description, string category, TransactionType type, 
        decimal amount, DateTime movementDate, DateTime dueDate) 
        : base(description, category, type, amount, movementDate, dueDate)
    {
    }
}
