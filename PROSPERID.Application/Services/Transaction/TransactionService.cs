using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Application.ModelViews;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Core.Interface.Repositories;

namespace PROSPERID.Application.Services.Transaction;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    private readonly ICategoryRepository _categoryRepository;
    public TransactionService(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository)
    {
        _repository = transactionRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ServiceResponse<TransactionView>> GetTransactionByIdAsync(Guid id)
    {
        try
        {
            TransactionView transactionView = await _repository.GetTransactionByIdAsync(id);
            if (transactionView == null)
                return ServiceResponseHelper.Error<TransactionView>(404, "Transação não foi localizada");
            return ServiceResponseHelper.Success<TransactionView>(200, "Busca realizada com sucesso!", transactionView);
        }
        catch
        {
            return ServiceResponseHelper.Error<TransactionView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<IEnumerable<TransactionView>>> GetTransactionsAsync()
    {
        try
        {
            var transaction = await _repository.GetTransactionsAsync();
            if (!transaction.Any())
                return ServiceResponseHelper.Error<IEnumerable<TransactionView>>(404, "Transação não foi localizada");
            List<TransactionView> transactionViews = new();
            foreach (var item in transaction)
                transactionViews.Add(item);
            return ServiceResponseHelper.Success(200, "Busca realizada com sucesso!", (IEnumerable<TransactionView>)transactionViews);
        }
        catch
        {
            return ServiceResponseHelper.Error<IEnumerable<TransactionView>>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<TransactionView>> CreateTransactionAsync(CreateTransactionDTO createTransaction)
    {
        var validate = ValidateTransaction<CreateTransactionDTO>
            .Validate(createTransaction);
        if (validate != null)
            return ServiceResponseHelper.Error<TransactionView>(validate.Status, validate.Message);
        var category = await _categoryRepository.GetCategoryByIdAsync(createTransaction.CategoryDTO.Id);
        if (category == null)
            return ServiceResponseHelper.Error<TransactionView>(400, "Não foi Encontrado a Categoria Vinculada a Transação!");
        var transaction = new Core.Entities.Transaction(createTransaction.Description,
            category, createTransaction.Type, createTransaction.Amount,
            createTransaction.TransactionDate, createTransaction.DueDate);
        if (await _repository.ExistsTransaction(transaction))
            return ServiceResponseHelper.Error<TransactionView>(400, "Já existe Movimento cadastrado com as mesmas informações!");
        try
        {
            TransactionDTO result = await _repository
                .CreateTransactionAsync(transaction);
            TransactionView transactionView = result;
            return ServiceResponseHelper.Success(200, "Movimento criado com sucesso!", transactionView);
        }
        catch
        {
            return ServiceResponseHelper.Error<TransactionView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<TransactionView>> UpdateTransactionAsync(UpdateTransactionDTO updateTransactionDTO)
    {
        var validate = ValidateTransaction<UpdateTransactionDTO>
             .Validate(updateTransactionDTO);
        if (validate != null)
            return ServiceResponseHelper.Error<TransactionView>(validate.Status, validate.Message);
        Core.Entities.Transaction transaction = await _repository.GetTransactionByIdAsync(updateTransactionDTO.Id);
        if (transaction == null)
            return ServiceResponseHelper.Error<TransactionView>(404, "Requisição inválida, Transação não encontrada");

        transaction.Update(updateTransactionDTO.Description, updateTransactionDTO.CategoryDTO.Id, updateTransactionDTO.Type,
            updateTransactionDTO.Amount, updateTransactionDTO.TransactionDate, updateTransactionDTO.DueDate);
        try
        {
            TransactionDTO result = await _repository.UpdateTransactionAsync(transaction);
            TransactionView transactionView = result;
            return ServiceResponseHelper.Success(200, "Transação alterada com sucesso!", transactionView);
        }
        catch
        {
            return ServiceResponseHelper.Error<TransactionView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<TransactionView>> DeleteTransactionAsync(Guid id)
    {
        Core.Entities.Transaction transaction = await _repository.GetTransactionByIdAsync(id);
        if (transaction == null)
            return ServiceResponseHelper.Error<TransactionView>(404, "Transação não foi localizada");
        if (transaction.PaymentDate is not null)
            return ServiceResponseHelper.Error<TransactionView>(400, "Transação está paga, não pode ser deletada!");
        try
        {
            var transactionDeleted = await _repository.DeleteTransactionAsync(id);
            return ServiceResponseHelper.Success<TransactionView>(200, "Transação deletada!");
        }
        catch
        {
            return ServiceResponseHelper.Error<TransactionView>(500, "Erro interno!");
        }
    }
}
