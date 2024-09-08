namespace PROSPERID.Core.Enums;

/// <summary>
/// Tipos de transação.
/// Utilize 0 para pagamento
/// Utilize 1 para recebimento
/// </summary>
public enum TransactionType 
{
    /// <summary>
    /// Pagamento.
    /// </summary>  
    /// <example>
    /// 0
    /// </example>
    Payment,

    /// <summary>
    /// Recebimento.
    /// </summary>
    /// <example>
    /// 1
    /// </example>
    Receipt
}
