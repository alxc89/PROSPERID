﻿using PROSPERID.Application.DTOs.CreditCardBill;
using PROSPERID.Core.Entities;
using PROSPERID.Core.ValueObjects;

namespace PROSPERID.Application.DTOs.CreditCard;

public class CreditCardDTO
{
    public CardNumber Number { get; set; } = new CardNumber("");
    public string HolderName { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public Money CreditLimit { get; set; } = new Money(0);
    public Money CurrentBalance { get; set; } = new Money(0);
    public DateTime DueDate { get; set; }

    public virtual ICollection<CreditCardBillDTO>? CreditCardBillDTO { get; set; }

    public PaymentMethod? PaymentMethod { get; set; }

    public static implicit operator CreditCardDTO(Core.Entities.CreditCard creditCard)
    {
        ICollection<CreditCardBillDTO> creditCardBillDTOs = [];
        foreach (var item in creditCard.CreditCardBill)
            creditCardBillDTOs.Add(item);

        CreditCardDTO CreditCardDTO = new()
        {
            Number = creditCard.Number,
            HolderName = creditCard.HolderName,
            ExpirationDate = creditCard.ExpirationDate,
            CreditLimit = creditCard.CreditLimit,
            CurrentBalance = creditCard.CurrentBalance,
            DueDate = creditCard.DueDate,
            CreditCardBillDTO = creditCardBillDTOs,
            PaymentMethod = creditCard.PaymentMethod
        };
        return CreditCardDTO;
    }
}
