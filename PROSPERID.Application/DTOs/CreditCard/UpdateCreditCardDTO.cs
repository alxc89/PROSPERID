﻿using PROSPERID.Application.DTOs.CreditCardBill;
using PROSPERID.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace PROSPERID.Application.DTOs.CreditCard;

public class UpdateCreditCardDTO : CreditCardDTO
{
    public long Id { get; set; }

    public ICollection<CreditCardBillDTO>? CreditCardBillDTO { get; set; }

    //public PaymentMethod? PaymentMethod { get; set; }

    public static implicit operator UpdateCreditCardDTO(Core.Entities.CreditCard creditCard)
    {
        ICollection<CreditCardBillDTO> creditCardBillDTOs = [];
        foreach (var item in creditCard.CreditCardBills)
            creditCardBillDTOs.Add(item);

        UpdateCreditCardDTO updateCreditCardDTO = new()
        {
            Number = creditCard.Number.Value,
            HolderName = creditCard.HolderName,
            ExpirationDate = creditCard.ExpirationDate,
            CreditLimit = creditCard.CreditLimit,
            CurrentBalance = creditCard.CurrentBalance,
            DueDate = creditCard.DueDate,
            CreditCardBillDTO = creditCardBillDTOs
        };
        return updateCreditCardDTO;
    }
}
