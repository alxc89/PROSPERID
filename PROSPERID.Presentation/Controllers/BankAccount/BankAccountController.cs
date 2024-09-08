using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using PROSPERID.Application.DTOs.BankAccount;
using PROSPERID.Application.ModelViews.BankAccount;
using PROSPERID.Application.Services.BankAccount;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PROSPERID.Presentation.Controllers.BankAccount;

[Route("api/[controller]")]
[ApiController]
public class BankAccountController(IBankAccountService bankAccountService) : ControllerBase
{
    private readonly IBankAccountService _bankAccountService = bankAccountService;

    /// <summary>
    /// Retorna uma Conta Bancária
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(BankAccountView), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(long id)
    {
        var bankAccount = await _bankAccountService.GetBankAccountByIdAsync(id);
        if (bankAccount.Data == null)
            return BadRequest(bankAccount?.Message);
        return Ok(bankAccount);
    }

    /// <summary>
    /// Buscar uma lista de Contas Bancárias.
    /// </summary>
    [HttpGet()]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(BankAccountView), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        var bankAccount = await _bankAccountService.GetBankAccountsAsync();
        if (bankAccount.Data == null && !bankAccount.IsSuccess)
            return BadRequest(bankAccount?.Message);
        return Ok(bankAccount);
    }

    /// <summary>
    /// Criação de uma Conta Bancária
    /// </summary>
    /// <param name="createbankAccountDTO"></param>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(BankAccountView), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(CreateBankAccountDTO createbankAccountDTO)
    {
        var newbankAccount = await _bankAccountService.CreateBankAccountAsync(createbankAccountDTO);
        if (!newbankAccount.IsSuccess)
            return BadRequest(newbankAccount.Message);
        return CreatedAtAction("Get", new { id = newbankAccount.Data?.Id }, newbankAccount.Data);
    }

    /// <summary>
    /// Alteração de uma Conta Bancária
    /// </summary>
    /// <param name="updatebankAccountDTO"></param>
    [HttpPut()]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(BankAccountView), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(UpdateBankAccountDTO updatebankAccountDTO)
    {
        var updatebankAccount = await _bankAccountService.UpdateBankAccountAsync(updatebankAccountDTO);
        if (!updatebankAccount.IsSuccess)
            return BadRequest(updatebankAccount?.Message);
        return Ok(updatebankAccount);
    }

    /// <summary>
    /// Deletar uma Conta Bancária
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(long id)
    {
        var updatebankAccount = await _bankAccountService.DeleteBankAccountAsync(id);
        if (!updatebankAccount.IsSuccess)
            return BadRequest(updatebankAccount?.Message);
        return Ok(updatebankAccount.Message);
    }
}
