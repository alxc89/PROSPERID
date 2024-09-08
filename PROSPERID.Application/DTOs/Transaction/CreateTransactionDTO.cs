using PROSPERID.Application.DTOs.Category;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class CreateTransactionDTO(string description, TransactionType type,
    decimal amount, DateTime transactionDate, DateTime dueDate, long categoryId) 
    : TransactionDTO(description, type, amount, transactionDate, dueDate, categoryId)
{
}