using PROSPERID.Application.DTOs.Transaction;
using Entities = PROSPERID.Core.Entities;
using PROSPERID.Core.Enums;

namespace PROSPERID.Application.ModelViews.Transaction;
public class TransactionView
{
    /// <summary>
    /// Id da Transação.
    /// </summary>
    /// <example>1</example>
    public long Id { get; set; }

    /// <summary>
    /// Descrição da Transação
    /// </summary>
    /// <example>Mensalidade escolar</example>
    public string Description { get; set; }

    /// <summary>
    /// Tipo da transação, indica se é pagamento ou recebimento.
    /// </summary>
    /// <example>
    /// ["Pagamento", "Recebimento"]
    /// </example>
    public TransactionType Type { get; set; }

    /// <summary>
    /// Valor da transação.
    /// </summary>
    /// <example>500.00</example>
    public decimal Amount { get; set; }

    /// <summary>
    /// Data da Transação.
    /// </summary>
    /// <example>2024-09-01</example>
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// Data de pagamento da transação.
    /// </summary>
    /// <example>2024-09-07</example>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Id da Categoria.
    /// </summary>
    /// <example>1</example>
    public long CategoryId { get; set; }

    public TransactionView(long id, string description, long categoryId, TransactionType type, decimal amount, DateTime transactionDate, DateTime dueDate)
    {
        Id = id;
        Description = description;
        CategoryId = categoryId;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDate;
        DueDate = dueDate;
    }

    public TransactionView(string description, long categoryId, TransactionType type, decimal amount, DateTime transactionDate, DateTime dueDate)
    {
        Description = description;
        CategoryId = categoryId;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDate;
        DueDate = dueDate;
    }

    public static implicit operator TransactionView(UpdateTransactionDTO transaction)
    {
        return new TransactionView(transaction.Id, transaction.Description, transaction.CategoryId, transaction.Type,
            transaction.Amount, transaction.TransactionDate, transaction.DueDate);
    }

    public static implicit operator TransactionView(Entities.Transaction transaction)
    {
        return new TransactionView(transaction.Id, transaction.Description, transaction.CategoryId, transaction.Type,
            transaction.Amount, transaction.TransactionDate, transaction.DueDate);
    }

    //public static implicit operator TransactionView(CreateTransactionDTO transaction)
    //{
    //    CategoryDTO categoryDTO = new(transaction.Category.Id, transaction.Category.Name);
    //    return new TransactionView(transaction.Description, categoryDTO, transaction.Type,
    //        transaction.Amount, transaction.TransactionDate, transaction.DueDate);
    //}

    public static implicit operator TransactionView(TransactionDTO transaction)
    {
        return new TransactionView(transaction.Description, transaction.CategoryId, transaction.Type,
            transaction.Amount, transaction.TransactionDate, transaction.DueDate);
    }
}