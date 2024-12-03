using Amazon.Lambda.Core;
using EliminarPersona.Context;
using EliminarPersona.Mediator;
using EliminarPersona.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace EliminarPersona;

public class Function
{
    private readonly IServiceProvider _serviceProvider;

    public Function()
    {
        var services = new ServiceCollection();

        var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(EliminarPersonaCommandHandler).Assembly));

        _serviceProvider = services.BuildServiceProvider();
    }


    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public async Task<PersonaResponse> FunctionHandler(EliminarPersonaCommand input, ILambdaContext context)
    {
        try
        {            
            var mediator = _serviceProvider.GetRequiredService<IMediator>();            
            return await mediator.Send(input);
        }

        catch (Exception ex)
        {            
            return new PersonaResponse
            {
                Error = "500",
                MensajeError = $"Error al procesar la solicitud: {ex.Message}"              
            };
        }
    }
}
