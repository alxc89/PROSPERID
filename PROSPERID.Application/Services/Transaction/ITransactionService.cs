using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Application.Services.Shared;

namespace PROSPERID.Application.Services.Transaction;

public interface ITransactionService
{
    Task<ServiceResponse<TransactionDTO>> CreateTransactionAsync(CreateTransactionDTO createTransactionDTO);
    Task<ServiceResponse<TransactionDTO>> UpdateTransactionAsync(UpdateTransactionDTO updateTransactionDTO);
    Task<ServiceResponse<TransactionDTO>> GetTransactionByIdAsync(Guid id);
    Task<ServiceResponse<IEnumerable<TransactionDTO>>> GetTransactionsAsync();
    Task<ServiceResponse<TransactionDTO>> DeleteTransactionAsync(Guid id);
}
