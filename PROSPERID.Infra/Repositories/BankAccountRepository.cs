﻿using Microsoft.EntityFrameworkCore;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Interface.Repositories;
using PROSPERID.Infra.Context;

namespace PROSPERID.Infra.Repositories;

public class BankAccountRepository(DataContext context) : IBankAccountRepository
{
    private readonly DataContext _context = context;

    public async Task<BankAccount> CreateBankAccountAsync(BankAccount bankAccount)
    {
        try
        {
            await _context
                .BankAccounts
                .AddAsync(bankAccount);
            await _context.SaveChangesAsync();
            return bankAccount;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }
    public async Task<IEnumerable<BankAccount>> GetBankAccountsAsync()
    {
        return await _context
            .BankAccounts
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<BankAccount?> GetBankAccountByIdAsync(long id)
    {
        var bankAccount = await _context
            .BankAccounts
            .SingleOrDefaultAsync(b => b.Id == id);
        return bankAccount;
    }
    public async Task<bool> AnyBankAccountAsync(string accountNumber)
    {
        return await _context
            .BankAccounts
            .AnyAsync(c => c.AccountNumber == accountNumber);
    }
    public async Task<BankAccount?> UpdateBankAccountAsync(BankAccount bankAccount)
    {
        var bankAccountUpdated = await _context
            .BankAccounts
            .SingleOrDefaultAsync(b => b.Id == bankAccount.Id);
        if (bankAccountUpdated == null)
            return null;
        try
        {
            _context
                .Entry(bankAccountUpdated)
                .CurrentValues
                .SetValues(bankAccount);
            await _context.SaveChangesAsync();
            return bankAccountUpdated;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }
    public async Task DeleteBankAccountAsync(long id)
    {
        var bankAccountDeleted = await _context
            .BankAccounts
            .SingleOrDefaultAsync(b => b.Id == id);

        if (bankAccountDeleted == null) return;

        try
        {
            _context.BankAccounts.Remove(bankAccountDeleted);
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }
    public async Task<bool> AnyMovementInAccount(string accountNumber)
    {
        var bankAccount = await _context
            .BankAccounts
            .Include(t => t.Transactions)
                .Where(t => t.AccountNumber == accountNumber)
            .SingleOrDefaultAsync();

        if (bankAccount?.Transactions?.Count == 0) return false;

        return true;
    }
    public async Task<bool> VerifyIfExistsAccount(string accountNumber)
    {
        return await _context
            .BankAccounts
            .AsNoTracking()
            .AnyAsync(b => b.AccountNumber == accountNumber);
    }
}
