using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROSPERID.Application.Services.BankAccount;
using PROSPERID.Application.Services.Category;
using PROSPERID.Application.Services.CreditCard;
using PROSPERID.Application.Services.Payment;
using PROSPERID.Application.Services.Transaction;
using PROSPERID.Core.Interface.Repositories;
using PROSPERID.Infra.Context;
using PROSPERID.Infra.Repositories;

namespace PROSPERID.Presentation.Commom.Api;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = builder
            .Configuration
            .GetConnectionString("DefaultConnection") ?? string.Empty;
    }

    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();
        builder.Services.AddAuthorization();
    }

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddDbContext<DataContext>(x =>
            {
                x.UseSqlServer(Configuration.ConnectionString,
                x => x.MigrationsAssembly("PROSPERID.Presentation"));
            });
    }

    public static void AddDependecyInjectionConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<IBankAccountRepository, BankAccountRepository>();
        builder.Services.AddTransient<IBankAccountService, BankAccountService>();
        builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
        builder.Services.AddTransient<ITransactionService, TransactionService>();
        builder.Services.AddTransient<ICreditCardService, CreditCardService>();
        builder.Services.AddTransient<ICreditCardRepository, CreditCardRepository>();
        builder.Services.AddTransient<IPaymentService, PaymentService>();
    }

    //public static void AddCroosOringin(this WebApplicationBuilder builder)
    //{
    //    builder.Services.AddCors(opt
    //        => opt.AddPolicy(ApiConfiguration.CorsPolicyName,
    //        policy => policy
    //            .WithOrigins([Configuration.BackendUrl, Configuration.FrontendUrl])
    //            .AllowAnyMethod()
    //            .AllowAnyHeader()
    //            .AllowCredentials()
    //        ));
    //}
}