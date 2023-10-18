using PROSPERID.Domain.Entities;

namespace PROSPERID.Domain.Interface.Repositories;

public interface IFinancialMovementRepository
{
    Task<FinancialMovement> CreateFinancialMovementAsync(FinancialMovement financialMovement);
    Task<IEnumerable<FinancialMovement>> GetFinancialMovementsAsync();
    Task<FinancialMovement> GetFinancialMovementByIdAsync(Guid id);
    Task<FinancialMovement> UpdateFinancialMovementAsync(FinancialMovement financialMovement);
    Task<FinancialMovement> DeleteFinancialMovementAsync(Guid id);
    Task<bool> ExistsFinancialMovement(FinancialMovement financialMovement);
}
