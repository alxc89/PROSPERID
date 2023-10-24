using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Domain.Interface.Repositories;

namespace PROSPERID.Application.Services.Transaction;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    public TransactionService(ITransactionRepository transactionRepository)
        => _repository = transactionRepository;

    public async Task<ServiceResponse<TransactionDTO>> GetTransactionByIdAsync(Guid id)
    {
        try
        {
            TransactionDTO transactionDTO = await _repository.GetTransactionByIdAsync(id);
            if (transactionDTO == null)
                return ServiceResponseHelper.Error<TransactionDTO>(404, "Transação não foi localizada");
            return ServiceResponseHelper.Success(200, "Busca realizada com sucesso!", transactionDTO);
        }
        catch
        {
            return ServiceResponseHelper.Error<TransactionDTO>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<IEnumerable<TransactionDTO>>> GetTransactionsAsync()
    {
        try
        {
            IEnumerable<TransactionDTO> transactionDTO = (IEnumerable<TransactionDTO>)await _repository.GetTransactionsAsync();
            if (!transactionDTO.Any())
                return ServiceResponseHelper.Error<IEnumerable<TransactionDTO>>(404, "Transação não foi localizada");
            return ServiceResponseHelper.Success(200, "Busca realizada com sucesso!", transactionDTO);
        }
        catch
        {
            return ServiceResponseHelper.Error<IEnumerable<TransactionDTO>>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<TransactionDTO>> CreateTransactionAsync(CreateTransactionDTO createTransaction)
    {
        var validate = ValidateTransaction<CreateTransactionDTO>
            .Validate(createTransaction);
        if (validate != null)
            return ServiceResponseHelper.Error<TransactionDTO>(validate.Status, validate.Message);
        var transaction = new Domain.Entities.Transaction(createTransaction.Description,
            createTransaction.Category, createTransaction.Type, createTransaction.Amount,
            createTransaction.TransactionDate, createTransaction.DueDate);
        if (await _repository.ExistsTransaction(transaction))
            return ServiceResponseHelper.Error<TransactionDTO>(400, "Já existe Movimento cadastrado com as mesmas informações!");
        try
        {
            TransactionDTO result = await _repository
                .CreateTransactionAsync(transaction);
            return ServiceResponseHelper.Success(200, "Movimento criado com sucesso!", result);
        }
        catch
        {
            return ServiceResponseHelper.Error<TransactionDTO>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<TransactionDTO>> UpdateTransactionAsync(UpdateTransactionDTO updateTransactionDTO)
    {
        var validate = ValidateTransaction<UpdateTransactionDTO>
             .Validate(updateTransactionDTO);
        if (validate != null)
            return ServiceResponseHelper.Error<TransactionDTO>(validate.Status, validate.Message);
        Domain.Entities.Transaction transaction = await _repository.GetTransactionByIdAsync(updateTransactionDTO.Id);
        if (transaction == null)
            return ServiceResponseHelper.Error<TransactionDTO>(404, "Requisição inválida, Transação não encontrada");
        transaction.Update(updateTransactionDTO.Description, updateTransactionDTO.Category, updateTransactionDTO.Type,
            updateTransactionDTO.Amount, updateTransactionDTO.TransactionDate, updateTransactionDTO.DueDate);
        try
        {
            TransactionDTO result = await _repository.UpdateTransactionAsync(transaction);
            return ServiceResponseHelper.Success(200, "Transação alterada com sucesso!", result);
        }
        catch
        {
            return ServiceResponseHelper.Error<TransactionDTO>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<TransactionDTO>> DeleteTransactionAsync(Guid id)
    {
        Domain.Entities.Transaction transaction = await _repository.GetTransactionByIdAsync(id);
        if (transaction == null)
            return ServiceResponseHelper.Error<TransactionDTO>(404, "Transação não foi localizada");
        if (transaction.PaymentDate is not null)
            return ServiceResponseHelper.Error<TransactionDTO>(400, "Transação está paga, não pode ser deletada!");
        try
        {
            var transactionDeleted = await _repository.DeleteTransactionAsync(id);
            return ServiceResponseHelper.Success<TransactionDTO>(200, "Transação deletada!");
        }
        catch
        {
            return ServiceResponseHelper.Error<TransactionDTO>(500, "Erro interno!");
        }
    }
}
