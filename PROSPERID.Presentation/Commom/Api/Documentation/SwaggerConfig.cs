using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace PROSPERID.Presentation.Commom.Api.Documentation;

public static class SwaggerConfig
{
    private static readonly string[] _namespaces =
    {
        "PROSPERID.Presentation",
        "PROSPERID.Application"
    };

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PROSPERID",
                Description = "API da Aplicação PROSPERID",
                Version = "1",
                Contact = new OpenApiContact
                {
                    Name = "Alex Cardoso",
                    Email = "alex.caardoso@hotmail.com",
                    Url = new Uri("https://github.com/alxc89")
                }
            });

            foreach (var item in _namespaces)
            {
                var xmlFile = $"{item}.xml";
                Console.WriteLine(xmlFile);
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            }

            x.EnableAnnotations();
        });
    }

    public static void AddConfigurationDevEnvironment(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = string.Empty;
            c.SwaggerEndpoint("./swagger/v1/swagger.json", "PROSPERID v1");
            c.DocExpansion(DocExpansion.List);
        });
    }
}
