using Microsoft.AspNetCore.Mvc;
using PROSPERID.Application.DTOs.Transaction;
using PROSPERID.Application.Services.Transaction;

namespace PROSPERID.Presentation.Controllers.Transaction;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    public TransactionController(ITransactionService transactionService)
        => _transactionService = transactionService;

    // GET: api/<TransactionController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var transactions = await _transactionService.GetTransactionsAsync();
        if (transactions.Data == null)
            return NotFound(transactions.Message);
        return Ok(transactions);
    }

    // GET api/<TransactionController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);
        if (transaction.Data == null)
            return NotFound(transaction.Message);
        return Ok(transaction);
    }

    // POST api/<TransactionController>
    [HttpPost]
    public async Task<IActionResult> Post(CreateTransactionDTO createTransactionDTO)
    {
        var newTransaction = await _transactionService.CreateTransactionAsync(createTransactionDTO);
        if (!newTransaction.IsSuccess)
            return BadRequest(newTransaction.Message);
        return CreatedAtAction("Get", new { id = newTransaction.Data?.Id }, newTransaction);
    }

    // PUT api/<TransactionController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(UpdateTransactionDTO updateTransactionDTO)
    {
        var updateTransaction = await _transactionService.UpdateTransactionAsync(updateTransactionDTO);
        if (!updateTransaction.IsSuccess)
            return BadRequest(updateTransaction.Message);
        return Ok(updateTransaction);
    }

    // DELETE api/<TransactionController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleteTransaction = await _transactionService.DeleteTransactionAsync(id);
        if (!deleteTransaction.IsSuccess)
            return BadRequest(deleteTransaction.Message);
        return NoContent();
    }
}
