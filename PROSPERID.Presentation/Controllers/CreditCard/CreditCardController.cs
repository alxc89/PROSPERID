using Microsoft.AspNetCore.Mvc;
using PROSPERID.Application.DTOs.CreditCard;
using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Application.ModelViews.CreditCard;
using PROSPERID.Application.ModelViews.Transaction;
using PROSPERID.Application.Services.CreditCard;
using PROSPERID.Application.Services.Shared;
using System.Net.Mime;

namespace PROSPERID.Presentation.Controllers.CreditCard;

[Route("api/[controller]")]
[ApiController]
public class CreditCardController(ICreditCardService creditCardService) : ControllerBase
{
    private readonly ICreditCardService _creditCardService = creditCardService;

    /// <summary>
    /// Criação de um Cartão de Crédito.
    /// </summary>
    /// <param name="createCreditCardDTO"></param>
    /// <returns></returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<CreditCardView>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(CreateCreditCardDTO createCreditCardDTO)
    {
        var newCreditCard = await _creditCardService.CreateCreditCardAsync(createCreditCardDTO);
        if (!newCreditCard.IsSuccess)
            return BadRequest(newCreditCard.Message);
        return CreatedAtAction("Get", new { id = newCreditCard.Data?.Id }, newCreditCard);
    }

    /// <summary>
    /// Retorna um Cartão de Crédito.
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<CreditCardView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(long id)
    {
        var creditCard = await _creditCardService.GetCreditCardByIdAsync(id);
        if (creditCard.Data == null)
            return NotFound(creditCard.Message);
        return Ok(creditCard);
    }
}
