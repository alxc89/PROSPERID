using PROSPERID.Application.DTOs.FinancialMovement;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Domain.Interface.Repositories;

namespace PROSPERID.Application.Services.FinancialMovement;

public class FinancialMovementService : IFinancialMovementService
{
    private readonly IFinancialMovementRepository _repository;
    public FinancialMovementService(IFinancialMovementRepository financialMovementRepository)
        => _repository = financialMovementRepository;

    public async Task<ServiceResponse<FinancialMovementDTO>> CreateFinancialMovementAsync(CreateFinancialMovementDTO createMovement)
    {
        var validate = ValidateFinancialMovement<CreateFinancialMovementDTO>
            .Validate(createMovement);
        if (validate != null)
            return ServiceResponseHelper.Error<FinancialMovementDTO>(validate.Status, validate.Message);
        var financialMovement = new Domain.Entities.FinancialMovement(createMovement.Description,
            createMovement.Category, createMovement.Type, createMovement.Amount,
            createMovement.MovementDate, createMovement.DueDate);
        if (await _repository.ExistsFinancialMovement(financialMovement))
            return ServiceResponseHelper.Error<FinancialMovementDTO>(400, "Já existe Movimento cadastrado com as mesmas informações!");
        try
        {
            FinancialMovementDTO result = await _repository
                .CreateFinancialMovementAsync(financialMovement);
            return ServiceResponseHelper.Success(200, "Movimento criado com sucesso!", result);
        }
        catch
        {
            return ServiceResponseHelper.Error<FinancialMovementDTO>(500, "Erro interno!");
        }
    }

    public Task<ServiceResponse<FinancialMovementDTO>> DeleteFinancialMovementAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<IEnumerable<FinancialMovementDTO>>> GetFinancialMovementsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<FinancialMovementDTO>> GetFinancialMovementByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<FinancialMovementDTO>> UpdateFinancialMovementAsync(UpdateFinancialMovementDTO updateFinancialMovementDTO)
    {
        throw new NotImplementedException();
    }
}
