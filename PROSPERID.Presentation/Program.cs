using PROSPERID.Presentation.Commom.Api;
using PROSPERID.Presentation.Commom.Api.Documentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.AddConfiguration();
builder.AddDataContexts();
builder.AddDependecyInjectionConfiguration();
builder.Services.AddCors();
builder.AddDocumentation();
//builder.Services.AddCors();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.AddConfigurationDevEnvironment();

app.UseDataBaseConfiguration();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
});

app.Run();
