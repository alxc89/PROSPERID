using Microsoft.AspNetCore.Mvc;
using PROSPERID.Application.DTOs.BankAccount;
using PROSPERID.Application.Services.BankAccount;

namespace PROSPERID.Presentation.Controllers.BankAccount;

[Route("api/[controller]")]
[ApiController]
public class BankAccountController : ControllerBase
{
    private readonly IBankAccountService _bankAccountService;
    public BankAccountController(IBankAccountService bankAccountService)
        => _bankAccountService = bankAccountService;
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var bankAccount = await _bankAccountService.GetBankAccountByIdAsync(id);
        if (bankAccount.Data == null)
            return BadRequest(bankAccount?.Message);
        return Ok(bankAccount);
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        var bankAccount = await _bankAccountService.GetBankAccountsAsync();
        if (bankAccount.Data == null && !bankAccount.IsSuccess)
            return BadRequest(bankAccount?.Message);
        return Ok(bankAccount);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateBankAccountDTO createbankAccountDTO)
    {
        var newbankAccount = await _bankAccountService.CreateBankAccountAsync(createbankAccountDTO);
        if (!newbankAccount.IsSuccess)
            return BadRequest(newbankAccount.Message);
        return CreatedAtAction("Get", new { id = newbankAccount.Data?.Id }, newbankAccount.Data);
    }

    [HttpPut()]
    public async Task<IActionResult> Put(UpdateBankAccountDTO updatebankAccountDTO)
    {
        var updatebankAccount = await _bankAccountService.UpdateBankAccountAsync(updatebankAccountDTO);
        if (!updatebankAccount.IsSuccess)
            return BadRequest(updatebankAccount?.Message);
        return Ok(updatebankAccount);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var updatebankAccount = await _bankAccountService.DeleteBankAccountAsync(id);
        if (!updatebankAccount.IsSuccess)
            return BadRequest(updatebankAccount?.Message);
        return Ok(updatebankAccount.Message);
    }
}
