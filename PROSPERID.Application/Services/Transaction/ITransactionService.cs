using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Application.ModelViews;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.Transaction;

public interface ITransactionService
{
    Task<ServiceResponse<TransactionView>> CreateTransactionAsync(CreateTransactionDTO createTransactionDTO);
    Task<ServiceResponse<TransactionView>> UpdateTransactionAsync(UpdateTransactionDTO updateTransactionDTO);
    Task<ServiceResponse<TransactionView>> GetTransactionByIdAsync(Guid id);
    Task<ServiceResponse<IEnumerable<TransactionView>>> GetTransactionsAsync();
    Task<ServiceResponse<TransactionView>> DeleteTransactionAsync(Guid id);
}
