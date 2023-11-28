using PROSPERID.Application.DTOs.Category;
using PROSPERID.Domain.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class CreateTransactionDTO : TransactionDTO
{
    public CreateTransactionDTO(string description, TransactionType type,
        decimal amount, DateTime transactionDate, DateTime dueDate, CategoryDTO categoryDTO)
        : base(description, type, amount, transactionDate, dueDate, categoryDTO)
    {
    }
}