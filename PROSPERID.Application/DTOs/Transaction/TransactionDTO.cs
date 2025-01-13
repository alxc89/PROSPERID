using PROSPERID.Core.Enums;

namespace PROSPERID.Application.DTOs.Transaction;

public class TransactionDTO
{
    public TransactionDTO(string description, ETransactionType type, decimal amount,
        DateTime transactionDate, DateTime dueDate, DateTime? paymentDate,
        long? paymentMethodId, long categoryId)
    {
        Description = description;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDate;
        DueDate = dueDate;
        PaymentDate = paymentDate;
        PaymentMethodId = paymentMethodId;
        CategoryId = categoryId;
        /*
         description, type, amount, transactionDate, 
        dueDate, categoryId, paymentDate, paymentMethodId*/
    }

    /// <summary>
    /// Descrição da Transação
    /// </summary>
    /// <example>Mensalidade escolar</example>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da transação, indica se é pagamento ou recebimento.
    /// </summary>
    /// <example>
    /// ["Pagamento", "Recebimento"]
    /// </example>
    public ETransactionType Type { get; set; } = ETransactionType.Payment;

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
    /// Data de vencimento da transação.
    /// </summary>
    /// <example>2024-09-07</example>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Data de pagamento da transação.
    /// </summary>
    /// <example>2024-09-07</example>
    public DateTime? PaymentDate { get; set; }

    /// <summary>
    /// Id do método de pagamento
    /// </summary>
    /// <example>1</example>
    public long? PaymentMethodId { get; set; }

    /// <summary>
    /// Id da Categoria.
    /// </summary>
    /// <example>1</example>
    public long CategoryId { get; set; }

    public static implicit operator TransactionDTO(Core.Entities.Transaction transaction)
    {
        return new TransactionDTO(transaction.Description, transaction.Type,
            transaction.Amount, transaction.TransactionDate, transaction.DueDate,
            transaction.PaymentDate, transaction.PaymentMethodId, transaction.CategoryId);
    }
}