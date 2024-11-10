using PROSPERID.Application.DTOs.Payment;
using PROSPERID.Application.ModelViews.Transaction;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Core.Interface.Repositories;

namespace PROSPERID.Application.Services.Payment;

public class PaymentService(ITransactionRepository transactionRepository,
    IPaymentMethodRepository paymentMethodRepository) : IPaymentService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository = paymentMethodRepository;

    public async Task<ServiceResponse<TransactionView>> ExecutePaymentAsync(long id, PaymentDTO paymentDTO)
    {
        try
        {
            var validate = ValidatePayment<PaymentDTO>.Validate(paymentDTO);
            if (validate != null)
                return ServiceResponseHelper.Error<TransactionView>(validate.Status, validate.Message);
            //buscar o tipo de pagamento
            var paymentMethod = await _paymentMethodRepository.GetPaymentMethodByIdAsync(paymentDTO.PaymentMethodId);
            if (paymentMethod != null)
                return ServiceResponseHelper.Error<TransactionView>(400, "Não foi localizado o Método de Pagamento!");
            if (!paymentMethod!.IsValid())
                return ServiceResponseHelper.Error<TransactionView>(400, "Método de Pagamento não é válido!");
            //ver qual tipo de pagamento
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null)
                return ServiceResponseHelper.Error<TransactionView>(400, "Não foi localizado essa transação!");

            if (paymentMethod.BankAccount != null)
                transaction.ExecutePayment(paymentMethod.BankAccount, paymentDTO.PaymentDate);

            var transactionPaid = await _transactionRepository.UpdateTransactionAsync(transaction);
            return ServiceResponseHelper.Success(200, "Transação paga!", (TransactionView)transactionPaid);
        }
        catch
        {
            return ServiceResponseHelper.Error<TransactionView>(500, "Erro interno!");
        }
    }
}
