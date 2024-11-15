using Microsoft.AspNetCore.Mvc;
using PROSPERID.Application.DTOs.PaymentMethod;
using PROSPERID.Application.ModelViews.PaymentMethod;
using PROSPERID.Application.Services.PaymentMethod;
using PROSPERID.Application.Services.Shared;
using System.Net.Mime;

namespace PROSPERID.Presentation.Controllers.PaymentMethod;

[Route("api/[controller]")]
[ApiController]
public class PaymentMethodController(IPaymentMethodService paymentMethodService) : ControllerBase
{
    private readonly IPaymentMethodService _paymentMethodService = paymentMethodService;

    /// <summary>
    /// Retorna uma lista de Método de Pagamento.
    /// </summary>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<PaymentMethodView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var paymentMethod = await _paymentMethodService.GetAllPaymentMethodAsync();
        if (paymentMethod.Data == null)
            return NotFound(paymentMethod.Message);
        return Ok(paymentMethod);
    }

    /// <summary>
    /// Retorna um método de pagamento.
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<PaymentMethodView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(long id)
    {
        var paymentMethod = await _paymentMethodService.GetPaymentMethodByIdAsync(id);
        if (paymentMethod.Data == null)
            return NotFound(paymentMethod.Message);
        return Ok(paymentMethod);
    }

    /// <summary>
    /// Criação de método de pagamento
    /// </summary>
    /// <param name="createPaymentMethodDTO"></param>
    /// <returns></returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<PaymentMethodView>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(CreatePaymentMethodDTO createPaymentMethodDTO)
    {
        var newPaymentMethod = await _paymentMethodService.CreatePaymentMethodAsync(createPaymentMethodDTO);
        if (!newPaymentMethod.IsSuccess)
            return BadRequest(newPaymentMethod.Message);
        return CreatedAtAction("Get", new { id = newPaymentMethod.Data?.Id }, newPaymentMethod);
    }

    /// <summary>
    /// Alteração de um Método de Pagamento.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatePaymentMethodDTO"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<PaymentMethodView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(long id, UpdatePaymentMethodDTO updatePaymentMethodDTO)
    {
        var updatePaymentMethod = await _paymentMethodService.UpdatePaymentMethodAsync(id, updatePaymentMethodDTO);
        if (!updatePaymentMethod.IsSuccess)
            return BadRequest(updatePaymentMethod.Message);
        return Ok(updatePaymentMethod);
    }

    /// <summary>
    /// Deletar uma Transação
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<PaymentMethodView>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(long id)
    {
        var deletePaymentMethod = await _paymentMethodService.DeletePaymentMethodAsync(id);
        if (!deletePaymentMethod.IsSuccess)
            return BadRequest(deletePaymentMethod.Message);
        return NoContent();
    }
}
