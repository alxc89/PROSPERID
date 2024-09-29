using Microsoft.AspNetCore.Mvc;
using PROSPERID.Application.DTOs.CreditCard;
using PROSPERID.Application.ModelViews.CreditCard;
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
    /// Retorna uma lista de Cartões de Crédito.
    /// </summary>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<IEnumerable<CreditCardView>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var listCreditsCard = await _creditCardService.GetCreditCardsAsync();
        if (listCreditsCard.Data == null)
            return NotFound(listCreditsCard.Message);
        return Ok(listCreditsCard);
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
    /// Alteração de um Cartão de Crédito.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateCreditCardDTO"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<CreditCardView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(long id, UpdateCreditCardDTO updateCreditCardDTO)
    {
        var updateCreditCard = await _creditCardService.UpdateCreditCardAsync(id, updateCreditCardDTO);
        if (!updateCreditCard.IsSuccess)
            return BadRequest(updateCreditCard.Message);
        return Ok(updateCreditCard);
    }

    /// <summary>
    /// Deletar um Cartão de Crédito.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<CreditCardView>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(long id)
    {
        var deleteCreditCard = await _creditCardService.DeleteCreditCardAsync(id);
        if (!deleteCreditCard.IsSuccess)
            return BadRequest(deleteCreditCard.Message);
        return NoContent();
    }
}
