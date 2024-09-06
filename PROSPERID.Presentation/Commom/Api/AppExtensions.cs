using PROSPERID.Infra.Context;

namespace PROSPERID.Presentation.Commom.Api;

public static class AppExtensions
{
    public static void UseDataBaseConfiguration(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        //context.Database.Migrate();
        //context.Database.EnsureCreated();
    }
}
