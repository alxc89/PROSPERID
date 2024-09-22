namespace PROSPERID.Application.DTOs.CreditCard;

public class CreateCreditCardDTO
{
    /// <summary>
    /// Número do Cartão Crédito/Débito.
    /// </summary>
    /// <example>4455</example>
    public string Number { get; set; } = string.Empty;
    
    /// <summary>
    /// Nome do Proprietário do Cartão Crédito/Débito.
    /// </summary>
    /// <example>José da Silva</example>
    public string HolderName { get; set; } = string.Empty;

    /// <summary>
    /// Data de Expiração do Cartão de Crédito/Débito.
    /// </summary>
    /// <example>2030-09-01</example>
    public DateTime ExpirationDate { get; set; } = DateTime.Now.Date;

    /// <summary>
    /// Limte de crédito do Cartão de Crédito/Débito.
    /// </summary>
    /// <example>10000.00</example>
    public decimal CreditLimit { get; set; }

    /// <summary>
    /// Valor Atual do Cartão de Crédito/Débito.
    /// </summary>
    /// <example>10000.00</example>
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// Data de Vencimento do Cartão de Crédito/Débito.
    /// </summary>
    /// <example>2024-09-01</example>
    public DateTime DueDate { get; set; } = DateTime.Now.Date;
}
