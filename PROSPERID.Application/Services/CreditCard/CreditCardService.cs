using PROSPERID.Application.DTOs.CreditCard;
using PROSPERID.Application.ModelViews.CreditCard;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Core.Interface.Repositories;

namespace PROSPERID.Application.Services.CreditCard;

public class CreditCardService(ICreditCardRepository creditCardRepository) : ICreditCardService
{
    private readonly ICreditCardRepository _repository = creditCardRepository;

    public async Task<ServiceResponse<CreditCardView>> CreateCreditCardAsync(CreateCreditCardDTO createCreditCardDTO)
    {
        var validate = ValidateCreditCardInput<CreateCreditCardDTO>.Validate(createCreditCardDTO.Number.Value, createCreditCardDTO.HolderName,
            createCreditCardDTO.ExpirationDate, createCreditCardDTO.CreditLimit, createCreditCardDTO.DueDate);

        if (validate != null)
            return ServiceResponseHelper.Error<CreditCardView>(validate.Status, validate.Message);

        if (await _repository.AnyCartCredit(createCreditCardDTO.Number.Value))
            return ServiceResponseHelper.Error<CreditCardView>(400, "Requisição inválida, Cartão já existente");

        var creditCard = new Core.Entities.CreditCard(createCreditCardDTO.Number, createCreditCardDTO.HolderName, createCreditCardDTO.ExpirationDate,
        createCreditCardDTO.CreditLimit, createCreditCardDTO.DueDate);

        try
        {
            CreditCardView createCreditCard = await _repository.CreateCreditCardAsync(creditCard);
            return ServiceResponseHelper.Success(200, "Cartão criado com sucesso!", createCreditCard);
        }
        catch
        {
            return ServiceResponseHelper.Error<CreditCardView>(500, "Erro Interno!");
        }
    }

    public Task<ServiceResponse<CreditCardView>> DeleteCreditCardAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<CreditCardView>> GetCreditCardByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<IEnumerable<CreditCardView>>> GetCreditCardsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<CreditCardView>> UpdateCreditCardAsync(long id, UpdateCreditCardDTO updateCreditCardDTO)
    {
        throw new NotImplementedException();
    }
}
