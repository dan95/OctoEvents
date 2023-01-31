using OctoEvents.API.Attributes;
using OctoEvents.CrossCutting.IoC.DI;
using OctoEvents.Domain.ViewModel;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSwaggerGenNewtonsoftSupport()
    .RegisterDbSettings()
    .AddLogging(x =>
    {
        x.AddSimpleConsole(c =>
        {
            c.IncludeScopes = true;
            c.SingleLine = true;
            c.TimestampFormat = "HH:mm:ss ";
        });
    })
    .RegisterApiServices()
    .RegisterMediatrServices()
    .RegisterAutoMapper()
    .RegisterValidators();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", nameof(OctoEvents));
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
