using PROSPERID.Domain.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class CreateTransactionDTO : TransactionDTO
{
    public CreateTransactionDTO(string description, string category, TransactionType type, 
        decimal amount, DateTime transactionDate, DateTime dueDate) 
        : base(description, category, type, amount, transactionDate, dueDate)
    {
    }
}
