using PROSPERID.Core.Entities;
using PROSPERID.Core.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class CreateTransactionDTO : TransactionDTO
{
    public CreateTransactionDTO(string description, ETransactionType type, decimal amount,
        DateTime transactionDate, DateTime dueDate, long categoryId, DateTime? paymentDate = null,
        long? paymentMethodId = null)
        : base(description, type, amount, transactionDate, dueDate, paymentDate,
            paymentMethodId, categoryId)
    {
    }
}

/*
 * string description, ETransactionType type, 
 * decimal amount, DateTime transactionDate, 
 * DateTime dueDate, long categoryId, 
 * DateTime? paymentDate = null, long? paymentMethodId = null
 */

//public class CreateTransactionDTO(string description, ETransactionType type,
//    decimal amount, DateTime transactionDate, DateTime dueDate, long categoryId, DateTime paymentDate, long a)
//    : TransactionDTO(description, type, amount, transactionDate, dueDate, categoryId, paymentDate)
//{
//    string description, ETransactionType type,
//    decimal amount, DateTime transactionDate, DateTime dueDate, long categoryId, DateTime paymentDate, long a
//}
