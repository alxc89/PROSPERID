using PROSPERID.Application.DTOs.BankAccount;
using PROSPERID.Application.DTOs.FinancialMovement;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.FinancialMovement;

public interface IFinancialMovementService
{
    Task<ServiceResponse<FinancialMovementDTO>> CreateFinancialMovementAsync(CreateFinancialMovementDTO createFinancialMovementDTO);
    Task<ServiceResponse<FinancialMovementDTO>> UpdateFinancialMovementAsync(UpdateFinancialMovementDTO updateFinancialMovementDTO);
    Task<ServiceResponse<FinancialMovementDTO>> GetFinancialMovementByIdAsync(Guid id);
    Task<ServiceResponse<IEnumerable<FinancialMovementDTO>>> GetFinancialMovementsAsync();
    Task<ServiceResponse<FinancialMovementDTO>> DeleteFinancialMovementAsync(Guid id);
}
