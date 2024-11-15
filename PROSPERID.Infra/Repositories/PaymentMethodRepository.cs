using Microsoft.EntityFrameworkCore;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Interface.Repositories;
using PROSPERID.Infra.Context;

namespace PROSPERID.Infra.Repositories;

public class PaymentMethodRepository(DataContext context) : IPaymentMethodRepository
{
    private readonly DataContext _context = context;

    public async Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethod paymentMethod)
    {
        try
        {
            await _context.PaymentMethods.AddAsync(paymentMethod);
            await _context.SaveChangesAsync();
            return paymentMethod;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task DeletePaymentMethodAsync(long id)
    {
        var paymentMethodDeleted = await _context.PaymentMethods.SingleOrDefaultAsync(x => x.Id == id);
        if (paymentMethodDeleted == null)
            return;

        try
        {
            _context.PaymentMethods.Remove(paymentMethodDeleted);
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<PaymentMethod?> GetPaymentMethodByIdAsync(long id)
    {
        try
        {
            var paymentMethod = await _context.PaymentMethods
                .Include(b => b.BankAccount)
                .SingleOrDefaultAsync(t => t.Id == id);
            return paymentMethod;
        }
        catch (Exception)
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync()
    {
        try
        {
            var paymentMethod = await _context.PaymentMethods.ToListAsync();
            return paymentMethod;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }

    public async Task<PaymentMethod> UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
    {
        var paymentMethodUpdate = await _context.PaymentMethods.SingleOrDefaultAsync(x => x.Id == paymentMethod.Id);
        if (paymentMethodUpdate == null)
            return null!;
        try
        {
            _context.Entry(paymentMethodUpdate).CurrentValues.SetValues(paymentMethod);
            await _context.SaveChangesAsync();
            return paymentMethodUpdate;
        }
        catch
        {
            throw new Exception("Erro interno!");
        }
    }
}
