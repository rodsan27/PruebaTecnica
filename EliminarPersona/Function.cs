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
    private IServiceProvider _serviceProvider;

    public void InitFunction(EliminarPersonaCommand input)
    {
        var services = new ServiceCollection();

        var connectionString = String.Format("server={0};user={1};database={2};port={3};password={4}", input.server, input.user, input.database, 3306, input.pass);
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
            InitFunction(input);
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
