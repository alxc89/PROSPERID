using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Domain.Interface.Repositories;

namespace PROSPERID.Application.Services.Transaction;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    public TransactionService(ITransactionRepository transactionRepository)
        => _repository = transactionRepository;

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

    public Task<ServiceResponse<TransactionDTO>> DeleteTransactionAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<IEnumerable<TransactionDTO>>> GetTransactionsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<TransactionDTO>> GetTransactionByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<TransactionDTO>> UpdateTransactionAsync(UpdateTransactionDTO updateTransactionDTO)
    {
        throw new NotImplementedException();
    }
}
