using PROSPERID.Application.Services.BankAccount;
using PROSPERID.Application.Services.Category;
using PROSPERID.Domain.Interface.Repositories;
using PROSPERID.Infra.Repositories;

namespace PROSPERID.Presentation.Configuration;

public static class DependecyInjectionConfig
{
    public static void AddDependecyInjectionConfiguration(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        services.AddScoped<IBankAccountService, BankAccountService>();
    }
}
