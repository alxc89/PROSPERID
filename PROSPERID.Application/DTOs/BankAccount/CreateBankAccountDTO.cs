using System.ComponentModel.DataAnnotations;

namespace PROSPERID.Application.DTOs.BankAccount;

public class CreateBankAccountDTO
{
    /// <summary>
    /// Numero Conta Bancária
    /// </summary>
    /// <example>123456-5</example>
    [Required]
    [MinLength(5)]
    [MaxLength(20)]
    public string AccountNumber { get; set; } = string.Empty;
    /// <summary>
    /// Proprietário da Conta Bancária
    /// </summary>
    /// <example>José da Silva</example>
    [Required]
    [MinLength(5)]
    [MaxLength(100)]
    public string AccountHolder { get; set; } = string.Empty;
    /// <summary>
    /// Numero Conta Bancária
    /// </summary>
    /// <example>1000.00</example>
    public decimal Balance { get; set; } = 0m;
}
