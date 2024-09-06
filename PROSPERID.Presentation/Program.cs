using PROSPERID.Presentation.Commom.Api;
using PROSPERID.Presentation.Commom.Api.Documentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.AddConfiguration();
builder.AddDataContexts();
builder.AddDependecyInjectionConfiguration();
builder.AddDocumentation();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.AddConfigurationDevEnvironment();

app.UseDataBaseConfiguration();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
