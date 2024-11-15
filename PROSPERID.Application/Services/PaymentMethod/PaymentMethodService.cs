using PROSPERID.Application.DTOs.PaymentMethod;
using PROSPERID.Application.ModelViews.PaymentMethod;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Core.Interface.Repositories;
using System.Collections;
using System.Collections.Generic;

namespace PROSPERID.Application.Services.PaymentMethod;

public class PaymentMethodService(IPaymentMethodRepository paymentMethodRepository) : IPaymentMethodService
{
    private readonly IPaymentMethodRepository _repository = paymentMethodRepository;

    public async Task<ServiceResponse<PaymentMethodView>> GetPaymentMethodByIdAsync(long id)
    {
        try
        {
            var paymentMethodView = await _repository.GetPaymentMethodByIdAsync(id)!;

            if (paymentMethodView == null)
                return ServiceResponseHelper.Error<PaymentMethodView>(404, "Não foi localizado o método de pagamento!");
            return ServiceResponseHelper.Success<PaymentMethodView>(200, "Busca realizada com sucesso!", paymentMethodView);
        }
        catch
        {
            return ServiceResponseHelper.Error<PaymentMethodView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<IEnumerable<PaymentMethodView>>> GetAllPaymentMethodAsync()
    {
        try
        {
            var paymentMethods = await _repository.GetPaymentMethodsAsync();
            if (!paymentMethods.Any())
                return ServiceResponseHelper.Error<IEnumerable<PaymentMethodView>>(404, "Nenhuma Método de Pagamento localizado");
            List<PaymentMethodView> listPaymentMethod = [];
            foreach (var paymentMethod in paymentMethods)
                listPaymentMethod.Add(paymentMethod);
            return ServiceResponseHelper.Success(200, "Busca realizada com sucesso!", (IEnumerable<PaymentMethodView>)listPaymentMethod);
        }
        catch
        {
            return ServiceResponseHelper.Error<IEnumerable<PaymentMethodView>>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<PaymentMethodView>> CreatePaymentMethodAsync(CreatePaymentMethodDTO createPaymentMethodDTO)
    {
        var validate = ValidatePaymentMethodInput<CreatePaymentMethodDTO>.Validate(createPaymentMethodDTO);

        if (validate != null)
            return ServiceResponseHelper.Error<PaymentMethodView>(validate.Status, validate.Message);

        var paymentMethod = new Core.Entities
            .PaymentMethod(createPaymentMethodDTO.Name, createPaymentMethodDTO.PaymentMethodType,
            (long)createPaymentMethodDTO.BankAccountId!);

        try
        {
            PaymentMethodView createdPaymentMethod = await _repository.CreatePaymentMethodAsync(paymentMethod);
            return ServiceResponseHelper.Success(200, "Método de pagamento criado com sucesso!", createdPaymentMethod);
        }
        catch
        {
            return ServiceResponseHelper.Error<PaymentMethodView>(500, "Erro Interno!");
        }
    }

    public async Task<ServiceResponse<PaymentMethodView>> UpdatePaymentMethodAsync(long id, UpdatePaymentMethodDTO updatePaymentMethodDTO)
    {
        var validate = ValidatePaymentMethodInput<UpdatePaymentMethodDTO>.Validate(updatePaymentMethodDTO);
        if (validate != null)
            return ServiceResponseHelper.Error<PaymentMethodView>(validate.Status, validate.Message);
        var paymentMethod = await _repository.GetPaymentMethodByIdAsync(id);
        if (paymentMethod == null)
            return ServiceResponseHelper.Error<PaymentMethodView>(404, "Requisição inválida, Método de pagamento não encontrado");
        paymentMethod.Update(updatePaymentMethodDTO.Name);
        try
        {
            PaymentMethodView result = await _repository.UpdatePaymentMethodAsync(paymentMethod);

            return ServiceResponseHelper.Success(200, "Método de Pagamento alterado com sucesso!", result);
        }
        catch
        {
            return ServiceResponseHelper.Error<PaymentMethodView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<PaymentMethodView>> DeletePaymentMethodAsync(long id)
    {
        var paymentMethod = await _repository.GetPaymentMethodByIdAsync(id);
        if (paymentMethod == null)
            return ServiceResponseHelper.Error<PaymentMethodView>(404, "Método de Pagamento não localizado!");
        if (paymentMethod.Transactions.Any())
            return ServiceResponseHelper.Error<PaymentMethodView>(404, "Não é possível excluir o Método de Pagamento, existe transações vinculadas!");

        try
        {
            await _repository.DeletePaymentMethodAsync(id);
            return ServiceResponseHelper.Success<PaymentMethodView>(200, "Método de pagamento deletado!");
        }
        catch
        {
            return ServiceResponseHelper.Error<PaymentMethodView>(500, "Erro interno!");
        }
    }
}
