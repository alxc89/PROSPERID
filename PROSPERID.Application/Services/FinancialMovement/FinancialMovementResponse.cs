using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.FinancialMovement;

public class FinancialMovementResponse : Response
{
    protected FinancialMovementResponse() { }

    public FinancialMovementResponse(string message, int status)
    {
        Message = message;
        Status = status;
    }

    public FinancialMovementResponse(string message, int status, FinancialMovementResponseData data)
    {
        Message = message;
        Status = status;
        Data = data;
    }

    public FinancialMovementResponseData? Data { get; set; }
}

public record FinancialMovementResponseData(string Description, string Category, string Type, decimal Amount,
    DateTime MovementDate, DateTime DueDate, DateTime PaymentDate);
