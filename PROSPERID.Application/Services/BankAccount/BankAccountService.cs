using PROSPERID.Application.DTOs.BankAccount;
using PROSPERID.Application.ModelViews.BankAccount;
using PROSPERID.Application.Services.Shared;
using PROSPERID.Core.Interface.Repositories;

namespace PROSPERID.Application.Services.BankAccount;

public class BankAccountService(IBankAccountRepository bankAccountRepository) : IBankAccountService
{
    private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;

    public async Task<ServiceResponse<BankAccountView>> GetBankAccountByIdAsync(long id)
    {
        try
        {
            var bankAccount = await _bankAccountRepository.GetBankAccountByIdAsync(id);
            if (bankAccount == null)
                return ServiceResponseHelper.Error<BankAccountView>(404, "Conta Bancária Não foi localizada!");
            return ServiceResponseHelper.Success(200, "Busca realizada com sucesso!", (BankAccountView)bankAccount);
        }
        catch
        {
            return ServiceResponseHelper.Error<BankAccountView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<IEnumerable<BankAccountView>>> GetBankAccountsAsync()
    {
        try
        {
            var bankAccounts = await _bankAccountRepository.GetBankAccountsAsync();
            List<BankAccountView> bankAccountsDTO = [];
            if (!bankAccounts.Any())
                return ServiceResponseHelper.Error<IEnumerable<BankAccountView>>(404, "Conta Bancária Não foi localizada!");
            foreach (var bankAccount in bankAccounts)
                bankAccountsDTO.Add(bankAccount);
            return ServiceResponseHelper.Success(200, "Busca realizada com sucesso!", (IEnumerable<BankAccountView>)bankAccountsDTO);
        }
        catch
        {
            return ServiceResponseHelper.Error<IEnumerable<BankAccountView>>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<BankAccountView>> CreateBankAccountAsync(CreateBankAccountDTO createBankAccountDTO)
    {
        var validate = ValidateBankAccountInput<BankAccountView>
            .Validate(createBankAccountDTO.AccountNumber, createBankAccountDTO.AccountHolder, createBankAccountDTO.Balance);
        if (validate != null)
            return ServiceResponseHelper.Error<BankAccountView>(validate.Status, validate.Message);
        if (await _bankAccountRepository.VerifyIfExistsAccount(createBankAccountDTO.AccountNumber))
            return new ServiceResponse<BankAccountView>
                ($"Requisição inválida, Conta Bancária com o Número {createBankAccountDTO.AccountNumber} já existente", 400);
        var bankcAccount = new Core.Entities.BankAccount(createBankAccountDTO.AccountNumber,
            createBankAccountDTO.AccountHolder, createBankAccountDTO.Balance);

        try
        {
            var createBankAccount = await _bankAccountRepository.CreateBankAccountAsync(bankcAccount);
            BankAccountView result = createBankAccount;
            return ServiceResponseHelper.Success(200, "Conta bancária criada com sucesso!", result);
        }
        catch
        {
            return ServiceResponseHelper.Error<BankAccountView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<BankAccountView>> UpdateBankAccountAsync(UpdateBankAccountDTO updateBankAccountDTO)
    {
        var validate = ValidateBankAccountInput<BankAccountView>
            .Validate(updateBankAccountDTO.AccountNumber, updateBankAccountDTO.AccountHolder, updateBankAccountDTO.Balance);
        if (validate != null)
            return ServiceResponseHelper.Error<BankAccountView>(validate.Status, validate.Message);
        Core.Entities.BankAccount? bankAccount =
            await _bankAccountRepository.GetBankAccountByIdAsync(updateBankAccountDTO.Id);
        if (bankAccount == null)
            return ServiceResponseHelper
                .Error<BankAccountView>(400, $"Não existe a Conta Bancária {updateBankAccountDTO.AccountNumber}");

        bankAccount.Update(updateBankAccountDTO.AccountNumber, updateBankAccountDTO.AccountHolder, updateBankAccountDTO.Balance);
        try
        {
            var result = await _bankAccountRepository.UpdateBankAccountAsync(bankAccount);
            return ServiceResponseHelper.Success<BankAccountView>(200, "Conta Bancária alterada com sucesso!", bankAccount);
        }
        catch
        {
            return ServiceResponseHelper.Error<BankAccountView>(500, "Erro interno!");
        }
    }

    public async Task<ServiceResponse<BankAccountView>> DeleteBankAccountAsync(long id)
    {
        try
        {
            var bankAccount = await _bankAccountRepository.GetBankAccountByIdAsync(id);
            if (bankAccount == null)
                return ServiceResponseHelper.Error<BankAccountView>(404, "Conta Bancária Não foi localizada!");
            if (await _bankAccountRepository.AnyMovementInAccount(bankAccount.AccountNumber))
                return ServiceResponseHelper.Error<BankAccountView>
                    (400, $"Não é possível deletar a Conta Bancária {bankAccount.AccountNumber}, está relacoinada a um Movimento Financeiro");

            await _bankAccountRepository.DeleteBankAccountAsync(id);
            return ServiceResponseHelper.Success<BankAccountView>(404, "A Conta Bancária foi deletada com Sucesso!");
        }
        catch
        {
            return ServiceResponseHelper.Error<BankAccountView>(500, "Erro interno!");
        }
    }
}