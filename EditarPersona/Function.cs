using Amazon.Lambda.Core;
using EditarPersona.Context;
using EditarPersona.Mediator;
using EditarPersona.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace EditarPersona;

public class Function
{
    private IServiceProvider _serviceProvider;
    public void InitFunction(EditarPersonaCommand input)
    {
        // Configurar el contenedor de dependencias
        var services = new ServiceCollection();

        // Configurar DbContext con cadena de conexión desde el entorno
        var connectionString = String.Format("server={0};user={1};database={2};port={3};password={4}", input.server, input.user, input.database, 3306, input.pass);
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // Configurar MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(EditarPersonaCommandHandler).Assembly));

        // Construir el proveedor de servicios
        _serviceProvider = services.BuildServiceProvider();
    }

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public async Task<PersonaResponse> FunctionHandler(EditarPersonaCommand input, ILambdaContext context)
    {
        try
        {
            // Obtener Mediator del contenedor
            InitFunction(input);
            var mediator = _serviceProvider.GetRequiredService<IMediator>();

            // Enviar el comando a través de Mediator
            return await mediator.Send(input);
        }
        catch (Exception ex)
        {
            // Manejar errores
            return new PersonaResponse
            {
                MensajeError = $"Error al procesar la solicitud: {ex.Message}"
            };
        }

    }
}
