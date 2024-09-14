using PROSPERID.Core.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class TransactionDTO(string description, ETransactionType type,
    decimal amount, DateTime transactionDate, DateTime dueDate, long categoryId)
{
    /// <summary>
    /// Descrição da Transação
    /// </summary>
    /// <example>Mensalidade escolar</example>
    public string Description { get; set; } = description;

    /// <summary>
    /// Tipo da transação, indica se é pagamento ou recebimento.
    /// </summary>
    /// <example>
    /// ["Pagamento", "Recebimento"]
    /// </example>
    public ETransactionType Type { get; set; } = type;

    /// <summary>
    /// Valor da transação.
    /// </summary>
    /// <example>500.00</example>
    public decimal Amount { get; set; } = amount;

    /// <summary>
    /// Data da Transação.
    /// </summary>
    /// <example>2024-09-01</example>
    public DateTime TransactionDate { get; set; } = transactionDate;

    /// <summary>
    /// Data de pagamento da transação.
    /// </summary>
    /// <example>2024-09-07</example>
    public DateTime DueDate { get; set; } = dueDate;

    /// <summary>
    /// Id da Categoria.
    /// </summary>
    /// <example>1</example>
    public long CategoryId { get; set; } = categoryId;

    public static implicit operator TransactionDTO(Core.Entities.Transaction transaction)
    {
        return new TransactionDTO(transaction.Description, transaction.Type,
            transaction.Amount, transaction.TransactionDate, transaction.DueDate, transaction.CategoryId);
    }
}