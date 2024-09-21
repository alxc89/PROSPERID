using PROSPERID.Application.DTOs.CreditCard;
using PROSPERID.Application.ModelViews.CreditCard;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.CreditCard;

public interface ICreditCardService
{
    Task<ServiceResponse<CreditCardView>> CreateCreditCardAsync(CreateCreditCardDTO createCreditCardDTO);
    Task<ServiceResponse<CreditCardView>> UpdateCreditCardAsync(long id, UpdateCreditCardDTO updateCreditCardDTO);
    Task<ServiceResponse<CreditCardView>> DeleteCreditCardAsync(long id);
    Task<ServiceResponse<CreditCardView>> GetCreditCardByIdAsync(long id);
    Task<ServiceResponse<IEnumerable<CreditCardView>>> GetCreditCardsAsync();
}
