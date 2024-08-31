using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PROSPERID.Presentation.Commom.Api.Documentation;

public class CustomSchemaFilter : ISchemaFilter
{
    private readonly string[] _namespaces =
    {
        "PROSPERID.Application.DTOs",
        "PROSPERID.Core.Enums"
    };

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == null || context.Type.FullName == null)
            return;

        if (_namespaces.Any(ns => context.Type.FullName.Contains(ns, StringComparison.OrdinalIgnoreCase)))
        {
            schema.Title = context.Type.Name;
        }
    }
}
