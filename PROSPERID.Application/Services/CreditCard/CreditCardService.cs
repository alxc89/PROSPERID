using PROSPERID.Application.DTOs.CreditCard;
using PROSPERID.Application.ModelViews.CreditCard;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Core.Interface.Repositories;
using PROSPERID.Core.ValueObjects;

namespace PROSPERID.Application.Services.CreditCard;

public class CreditCardService(ICreditCardRepository creditCardRepository) : ICreditCardService
{
    private readonly ICreditCardRepository _repository = creditCardRepository;

    public async Task<ServiceResponse<CreditCardView>> GetCreditCardByIdAsync(long id)
    {
        try
        {
            var creditCardView = await _repository.GetCreditCardByIdAsync(id)!;

            if (creditCardView == null)
                return ServiceResponseHelper.Error<CreditCardView>(404, "Cartão de Crédito não foi localizado");
            return ServiceResponseHelper.Success<CreditCardView>(200, "Busca realizada com sucesso!", creditCardView);
        }
        catch
        {
            return ServiceResponseHelper.Error<CreditCardView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<IEnumerable<CreditCardView>>> GetCreditCardsAsync()
    {
        try
        {
            var listCreditCards = await _repository.GetCreditCardsAsync();

            if (!listCreditCards.Any())
                return ServiceResponseHelper.Error<IEnumerable<CreditCardView>>(404, "Nenhum Cartão de Crédito localizado!");
            List<CreditCardView> creditCards = [];
            foreach (var creditCard in listCreditCards)
                creditCards.Add(creditCard);
            return ServiceResponseHelper.Success(200, "Busca realizada com sucesso!", (IEnumerable<CreditCardView>)creditCards);
        }
        catch
        {
            return ServiceResponseHelper.Error<IEnumerable<CreditCardView>>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<CreditCardView>> CreateCreditCardAsync(CreateCreditCardDTO createCreditCardDTO)
    {
        var validate = ValidateCreditCardInput<CreateCreditCardDTO>.Validate(createCreditCardDTO);

        if (validate != null)
            return ServiceResponseHelper.Error<CreditCardView>(validate.Status, validate.Message);

        if (await _repository.AnyCartCredit(createCreditCardDTO.Number))
            return ServiceResponseHelper.Error<CreditCardView>(400, "Requisição inválida, Cartão já existente");

        var creditCard = new Core.Entities.CreditCard(new CardNumber(createCreditCardDTO.Number),
            createCreditCardDTO.HolderName, createCreditCardDTO.ExpirationDate,
            new Money(createCreditCardDTO.CreditLimit), createCreditCardDTO.DueDate);

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

    public async Task<ServiceResponse<CreditCardView>> UpdateCreditCardAsync(long id, UpdateCreditCardDTO updateCreditCardDTO)
    {
        var validate = ValidateCreditCardInput<UpdateCreditCardDTO>.Validate(updateCreditCardDTO);
        if (validate != null)
            return ServiceResponseHelper.Error<CreditCardView>(validate.Status, validate.Message);
        var creditCard = await _repository.GetCreditCardByIdAsync(id);
        if (creditCard == null)
            return ServiceResponseHelper.Error<CreditCardView>(404, "Requisição inválida, Cartão de Crédito não encontrado");
        creditCard.Update(updateCreditCardDTO.Number, updateCreditCardDTO.HolderName, updateCreditCardDTO.ExpirationDate,
            updateCreditCardDTO.CreditLimit, updateCreditCardDTO.CurrentBalance, updateCreditCardDTO.DueDate);
        try
        {
            CreditCardView result = await _repository.UpdateCreditCardAsync(creditCard);
            return ServiceResponseHelper.Success(200, "Cartão de Crédito alterado com sucesso!", result);
        }
        catch
        {
            return ServiceResponseHelper.Error<CreditCardView>(500, "Erro interno!");
        }
    }

    public Task<ServiceResponse<CreditCardView>> DeleteCreditCardAsync(long id)
    {
        throw new NotImplementedException();
    }
}
