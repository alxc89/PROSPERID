using PROSPERID.Application.DTOs.CreditCardBill;
using PROSPERID.Application.ModelViews.CreditCardBill;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.CreditCardBill;

public interface ICreditCardBillService
{
    Task<ServiceResponse<CreditCardBillView>> GetCreditCardBillByIdAsync(long id);
    Task<ServiceResponse<IEnumerable<CreditCardBillView>>> GetCreditCardBillsAsync();
    Task<ServiceResponse<CreditCardBillView>> CreateCreditCardBillAsync(CreateCreditCardBillDTO createCreditCardBillDTO);
    Task<ServiceResponse<CreditCardBillView>> UpdateCreditCardBillAsync(long id, UpdateCreditCardBillDTO updateCreditCardBillDTO);
    Task<ServiceResponse<CreditCardBillView>> DeleteCreditCardBillAsync(long id);
}
