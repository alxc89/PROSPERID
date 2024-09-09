using Microsoft.AspNetCore.Mvc;
using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Application.ModelViews.Transaction;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Application.Services.Transaction;
using System.Net.Mime;

namespace PROSPERID.Presentation.Controllers.Transaction;

[Route("api/[controller]")]
[ApiController]
public class TransactionController(ITransactionService transactionService) : ControllerBase
{
    private readonly ITransactionService _transactionService = transactionService;

    /// <summary>
    /// Retorna uma lista de Transações.
    /// </summary>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<TransactionView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var transactions = await _transactionService.GetTransactionsAsync();
        if (transactions.Data == null)
            return NotFound(transactions.Message);
        return Ok(transactions);
    }

    /// <summary>
    /// Retorna uma Transação.
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<TransactionView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(long id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);
        if (transaction.Data == null)
            return NotFound(transaction.Message);
        return Ok(transaction);
    }

    /// <summary>
    /// Criação de uma Transação
    /// </summary>
    /// <param name="createTransactionDTO"></param>
    /// <returns></returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<TransactionView>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(CreateTransactionDTO createTransactionDTO)
    {
        var newTransaction = await _transactionService.CreateTransactionAsync(createTransactionDTO);
        if (!newTransaction.IsSuccess)
            return BadRequest(newTransaction.Message);
        return CreatedAtAction("Get", new { id = newTransaction.Data?.Id }, newTransaction);
    }

    /// <summary>
    /// Alteração de uma Transação.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateTransactionDTO"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<TransactionView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(long id, UpdateTransactionDTO updateTransactionDTO)
    {
        var updateTransaction = await _transactionService.UpdateTransactionAsync(id, updateTransactionDTO);
        if (!updateTransaction.IsSuccess)
            return BadRequest(updateTransaction.Message);
        return Ok(updateTransaction);
    }

    /// <summary>
    /// Deletar uma Transação
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServiceResponse<TransactionView>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(long id)
    {
        var deleteTransaction = await _transactionService.DeleteTransactionAsync(id);
        if (!deleteTransaction.IsSuccess)
            return BadRequest(deleteTransaction.Message);
        return NoContent();
    }
}
