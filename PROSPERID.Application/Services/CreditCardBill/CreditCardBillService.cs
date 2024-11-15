using PROSPERID.Application.DTOs.CreditCardBill;
using PROSPERID.Application.ModelViews.CreditCardBill;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Core.Interface.Repositories;

namespace PROSPERID.Application.Services.CreditCardBill;

public class CreditCardBillService(ICreditCardBillRepository repository, ICategoryRepository categoryRepository) : ICreditCardBillService
{
    private readonly ICreditCardBillRepository _repository = repository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<ServiceResponse<CreditCardBillView>> GetCreditCardBillByIdAsync(long id)
    {
        try
        {
            var creditCardBillView = await _repository.GetCreditCardBillByIdAsync(id)!;

            if (creditCardBillView == null)
                return ServiceResponseHelper.Error<CreditCardBillView>(404, "Não foi localizado a Fatura do Cartão de Crédito!");
            return ServiceResponseHelper.Success<CreditCardBillView>(200, "Busca realizada com sucesso!", creditCardBillView);
        }
        catch
        {
            return ServiceResponseHelper.Error<CreditCardBillView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<IEnumerable<CreditCardBillView>>> GetCreditCardBillsAsync()
    {
        try
        {
            var creditCardBillViews = await _repository.GetCreditCardBillsAsync();

            if (!creditCardBillViews.Any())
                return ServiceResponseHelper.Error<IEnumerable<CreditCardBillView>>(404, "Não foi localizado a Fatura do Cartão de Crédito!");
            return ServiceResponseHelper.Success<IEnumerable<CreditCardBillView>>(200, "Busca realizada com sucesso!", (IEnumerable<CreditCardBillView>)creditCardBillViews);
        }
        catch
        {
            return ServiceResponseHelper.Error<IEnumerable<CreditCardBillView>>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<CreditCardBillView>> CreateCreditCardBillAsync(CreateCreditCardBillDTO createCreditCardBillDTO)
    {
        var validate = ValidateCreditCardBillInput<CreateCreditCardBillDTO>.Validate(createCreditCardBillDTO);

        if (validate != null)
            return ServiceResponseHelper.Error<CreditCardBillView>(validate.Status, validate.Message);

        if (await _repository.GetBillByCompetenceMonth(createCreditCardBillDTO.BillingPeriod, (long)createCreditCardBillDTO.CreditCardId!))
            return ServiceResponseHelper.Error<CreditCardBillView>(400, "Requisição inválida, Já existe Fatura no criada para esse mês de competência!");

        var creditCardBill = new Core.Entities.CreditCardBill(createCreditCardBillDTO.CreditCardId, createCreditCardBillDTO.BillDate, createCreditCardBillDTO.DueDate,
            createCreditCardBillDTO.TotalAmount, createCreditCardBillDTO.PaidAmount, createCreditCardBillDTO.PaymentStatus);

        if (createCreditCardBillDTO.Transactions != null)
        {
            foreach (var transactionDTO in createCreditCardBillDTO.Transactions)
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(transactionDTO.CategoryId);
                Core.Entities.Transaction transaction = new(transactionDTO.Description, category!.Id, transactionDTO.Type,
                    transactionDTO.Amount, transactionDTO.TransactionDate, transactionDTO.DueDate);
                creditCardBill.AddTransaction(transaction);
            }
        }

        try
        {
            CreditCardBillView createCreditCard = await _repository.CreateCreditCardBillAsync(creditCardBill);
            return ServiceResponseHelper.Success(200, "Cartão criado com sucesso!", createCreditCard);
        }
        catch
        {
            return ServiceResponseHelper.Error<CreditCardBillView>(500, "Erro Interno!");
        }
    }

    public async Task<ServiceResponse<CreditCardBillView>> UpdateCreditCardBillAsync(long id, UpdateCreditCardBillDTO updateCreditCardBillDTO)
    {
        var validate = ValidateCreditCardBillInput<UpdateCreditCardBillDTO>.Validate(updateCreditCardBillDTO);
        if (validate != null)
            return ServiceResponseHelper.Error<CreditCardBillView>(validate.Status, validate.Message);
        var creditCardBill = await _repository.GetCreditCardBillByIdAsync(id);
        if (creditCardBill == null)
            return ServiceResponseHelper.Error<CreditCardBillView>(404, "Requisição inválida, Fatura do não encontrada!");

        creditCardBill.Update(updateCreditCardBillDTO.BillDate, updateCreditCardBillDTO.DueDate, updateCreditCardBillDTO.TotalAmount, updateCreditCardBillDTO.PaidAmount);

        try
        {
            CreditCardBillView result = await _repository.UpdateCreditCardBillAsync(creditCardBill);
            return ServiceResponseHelper.Success(200, "Fatura alterada com sucesso!", result);
        }
        catch
        {
            return ServiceResponseHelper.Error<CreditCardBillView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<CreditCardBillView>> DeleteCreditCardBillAsync(long id)
    {
        var creditCardBill = await _repository.GetCreditCardBillByIdAsync(id);

        if (creditCardBill is null)
            return ServiceResponseHelper.Error<CreditCardBillView>(404, "Requisição inválida, fatura não encontrada!");

        if (creditCardBill.IsPaid())
            return ServiceResponseHelper.Error<CreditCardBillView>(404, "Requisição inválida, fatura está paga!");

        try
        {
            await _repository.DeleteCreditCardBillAsync(creditCardBill);
            return ServiceResponseHelper.Success<CreditCardBillView>(200, "Fatura deletada");
        }
        catch
        {
            return ServiceResponseHelper.Error<CreditCardBillView>(500, "Erro interno!");
        }
    }
}
