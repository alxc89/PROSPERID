using Microsoft.AspNetCore.Mvc;
using PROSPERID.Application.DTOs.Payment;
using PROSPERID.Application.ModelViews.Transaction;
using PROSPERID.Application.Services.Payment;
using PROSPERID.Application.Services.Shared;
using System.Net.Mime;

namespace PROSPERID.Presentation.Controllers.Payment;

[Route("api/[controller]")]
[ApiController]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;

    /// <summary>
    /// Pagar uma Transação
    /// </summary>
    /// <param name="id"></param>
    /// <param name="paymentDTO"></param>
    /// <returns></returns>
    [HttpPost("{id}/pay")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<TransactionView>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ProcessPayment(long id, PaymentDTO paymentDTO)
    {
        var transactionPaid = await _paymentService.ExecutePaymentAsync(id, paymentDTO);
        if (!transactionPaid.IsSuccess)
            return BadRequest(transactionPaid.Message);
        return Ok(transactionPaid);
    }
}
