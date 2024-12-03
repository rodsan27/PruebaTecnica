using Amazon.Lambda.Core;
using ConsultarPersona.Context;
using ConsultarPersona.Mediator;
using ConsultarPersona.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ConsultarPersona;

public class Function
{
    private readonly IServiceProvider _serviceProvider;

    public Function()
    {
        // Configurar el contenedor de dependencias
        var services = new ServiceCollection();

        // Configurar DbContext con cadena de conexión desde el entorno
        var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // Configurar MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ConsultarPersonaQueryHandler).Assembly));

        // Construir el proveedor de servicios
        _serviceProvider = services.BuildServiceProvider();
    }

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public async Task<List<PersonaResponse>> FunctionHandler(ILambdaContext context)
    {
        try
        {
            var mediator = _serviceProvider.GetRequiredService<IMediator>();
           
            return await mediator.Send(new ConsultarPersonaQuery());
        }
        catch (Exception ex)
        {            
            context.Logger.LogLine($"Error: {ex.Message}");
            return new List<PersonaResponse>();
        }
    }
}
