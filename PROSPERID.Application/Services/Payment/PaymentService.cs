using PROSPERID.Application.DTOs.Payment;
using PROSPERID.Application.ModelViews.Transaction;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Core.Interface.Repositories;
using PROSPERID.Infra.Repositories;

namespace PROSPERID.Application.Services.Payment;

public class PaymentService(ITransactionRepository transactionRepository,
    IPaymentMethodRepository paymentMethodRepository, IBankAccountRepository bankAccountRepository) : IPaymentService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository = paymentMethodRepository;
    private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;


    public async Task<ServiceResponse<TransactionView>> ExecutePaymentAsync(long id, PaymentDTO paymentDTO)
    {
        try
        {
            var validate = ValidatePayment<PaymentDTO>.Validate(paymentDTO);
            if (validate != null)
                return ServiceResponseHelper.Error<TransactionView>(validate.Status, validate.Message);

            // Buscar o método de pagamento
            var paymentMethod = await _paymentMethodRepository.GetPaymentMethodByIdAsync(paymentDTO.PaymentMethodId);
            if (paymentMethod == null || !paymentMethod.IsValid())
                return ServiceResponseHelper.Error<TransactionView>(400, "Método de Pagamento não é válido ou não encontrado!");

            // Buscar a transação
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null)
                return ServiceResponseHelper.Error<TransactionView>(400, "Transação não encontrada!");

            // Executar o pagamento e atualizar a conta bancária, se aplicável
            if (paymentMethod.BankAccount != null)
            {
                transaction.ExecutePayment(paymentMethod.BankAccount, paymentDTO.PaymentDate);
                await _transactionRepository.UpdateTransactionAsync(transaction);
                await _bankAccountRepository.UpdateBankAccountAsync(paymentMethod.BankAccount);
            }

            //await _unitOfWork.SaveChangesAsync();  // Salva as mudanças no contexto

            return ServiceResponseHelper.Success(200, "Transação paga!", (TransactionView)transaction);
        }
        catch (Exception ex)
        {
            // Log exception if necessary
            return ServiceResponseHelper.Error<TransactionView>(500, "Erro interno!");
        }
    }
}
