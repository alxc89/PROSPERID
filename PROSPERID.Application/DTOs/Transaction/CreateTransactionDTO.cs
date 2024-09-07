using PROSPERID.Application.DTOs.Category;
using PROSPERID.Core.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class CreateTransactionDTO(string description, TransactionType type,
    decimal amount, DateTime transactionDate, DateTime dueDate, CategoryDTO categoryDTO) 
    : TransactionDTO(description, type, amount, transactionDate, dueDate, categoryDTO)
{
}