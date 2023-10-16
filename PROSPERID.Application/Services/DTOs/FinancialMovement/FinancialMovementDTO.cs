using PROSPERID.Domain.Enums;

namespace PROSPERID.Application.Services.DTOs.FinancialMovement;

public class FinancialMovementDTO
{
    public FinancialMovementDTO(string description, string category, TransactionType type,
        decimal amount, DateTime movementDate, DateTime dueDate)
    {
        Description = description;
        Category = category;
        Type = type;
        Amount = amount;
        MovementDate = movementDate;
        DueDate = dueDate;
    }

    public string Description { get; set; }
    public string Category { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime MovementDate { get; set; }
    public DateTime DueDate { get; set; }

    public static implicit operator FinancialMovementDTO(Domain.Entities.FinancialMovement financialMovement)
    {
        return new FinancialMovementDTO(financialMovement.Description, financialMovement.Category, financialMovement.Type,
            financialMovement.Amount, financialMovement.MovementDate, financialMovement.DueDate);
    }
}