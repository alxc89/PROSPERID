using PROSPERID.Application.DTOs.BankAccount;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Domain.Interface.Repositories;

namespace PROSPERID.Application.Services.BankAccount;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _bankAccountRepository;
    public BankAccountService(IBankAccountRepository bankAccountRepository)
        => _bankAccountRepository = bankAccountRepository;

    public async Task<ServiceResponse<BankAccountDTO>> GetBankAccountByIdAsync(Guid id)
    {
        try
        {
            BankAccountDTO bankAccount = await _bankAccountRepository.GetBankAccountByIdAsync(id);
            if (bankAccount == null)
                return ServiceResponseHelper.Error<BankAccountDTO>(404, "Conta Bancária Não foi localizada!");
            return ServiceResponseHelper.Success(200, "Busca realizada com sucesso!", bankAccount);
        }
        catch
        {
            return ServiceResponseHelper.Error<BankAccountDTO>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<IEnumerable<BankAccountDTO>>> GetBankAccountsAsync()
    {
        try
        {
            IEnumerable<BankAccountDTO> bankAccount = (IEnumerable<BankAccountDTO>)await _bankAccountRepository.GetBankAccountsAsync();
            if (bankAccount == null)
                return ServiceResponseHelper.Error<IEnumerable<BankAccountDTO>>(404, "Conta Bancária Não foi localizada!");
            return ServiceResponseHelper.Success(200, "Busca realizada com sucesso!", bankAccount);
        }
        catch
        {
            return ServiceResponseHelper.Error<IEnumerable<BankAccountDTO>>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<BankAccountDTO>> CreateBankAccountAsync(CreateBankAccountDTO createBankAccountDTO)
    {
        var validate = ValidateBankAccountInput<BankAccountDTO>
            .Validate(createBankAccountDTO.AccountNumber, createBankAccountDTO.AccountHolder, createBankAccountDTO.Balance);
        if (validate != null)
            return ServiceResponseHelper.Error<BankAccountDTO>(validate.Status, validate.Message);
        if (await _bankAccountRepository.VerifyIfExistsAccount(createBankAccountDTO.AccountNumber))
            return new ServiceResponse<BankAccountDTO>
                ($"Requisição inválida, Conta Bancária com o Número {createBankAccountDTO.AccountNumber} já existente", 400);
        var bankcAccount = new Domain.Entities.BankAccount(createBankAccountDTO.AccountNumber,
            createBankAccountDTO.AccountHolder, createBankAccountDTO.Balance);

        try
        {
            var createBankAccount = await _bankAccountRepository.CreateBankAccountAsync(bankcAccount);
            BankAccountDTO result = createBankAccount;
            return ServiceResponseHelper.Success(200, "Conta bancária criada com sucesso!", result);
        }
        catch
        {
            return ServiceResponseHelper.Error<BankAccountDTO>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<BankAccountDTO>> UpdateAccountAsync(UpdateBankAccountDTO updateBankAccountDTO)
    {
        var validate = ValidateBankAccountInput<BankAccountDTO>
            .Validate(updateBankAccountDTO.AccountNumber, updateBankAccountDTO.AccountHolder, updateBankAccountDTO.Balance);
        if (validate != null)
            return ServiceResponseHelper.Error<BankAccountDTO>(validate.Status, validate.Message);
        Domain.Entities.BankAccount bankAccount =
            await _bankAccountRepository.GetBankAccountByIdAsync(updateBankAccountDTO.Id);
        if (bankAccount == null)
            return ServiceResponseHelper
                .Error<BankAccountDTO>(400, $"Não existe a Conta Bancária {updateBankAccountDTO.AccountNumber}");

        bankAccount.Update(updateBankAccountDTO.AccountNumber, updateBankAccountDTO.AccountHolder, updateBankAccountDTO.Balance);
        try
        {
            BankAccountDTO result = await _bankAccountRepository.UpdateBankAccountAsync(bankAccount);
            return ServiceResponseHelper.Success<BankAccountDTO>(200, "Conta Bancária alterada com sucesso!", bankAccount);
        }
        catch
        {
            return ServiceResponseHelper.Error<BankAccountDTO>(500, "Erro interno!");
        }
    }
    
    public async Task<ServiceResponse<BankAccountDTO>> DeleteBankAccountAsync(Guid id)
    {
        try
        {
            BankAccountDTO bankAccount = await _bankAccountRepository.GetBankAccountByIdAsync(id);
            if (bankAccount == null)
                return ServiceResponseHelper.Error<BankAccountDTO>(404, "Conta Bancária Não foi localizada!");
            if (await _bankAccountRepository.AnyMovementInAccount(bankAccount.AccountNumber))
                return ServiceResponseHelper.Error<BankAccountDTO>
                    (400, $"Não é possível deletar a Conta Bancária {bankAccount.AccountNumber}, está relacoinada a um Movimento Financeiro");

            await _bankAccountRepository.DeleteBankAccountAsync(id);
            return ServiceResponseHelper.Success<BankAccountDTO>(404, "A Conta Bancária foi deletada com Sucesso!");
        }
        catch
        {
            return ServiceResponseHelper.Error<BankAccountDTO>(500, "Erro interno!");
        }
    }
}