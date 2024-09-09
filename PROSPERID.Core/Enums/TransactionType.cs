namespace PROSPERID.Core.Enums;

/// <summary>
/// Tipos de transação:
/// 0 para pagamento.
/// 1 para recebimento.
/// </summary>
/// <example>["1", "2"]</example>
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
