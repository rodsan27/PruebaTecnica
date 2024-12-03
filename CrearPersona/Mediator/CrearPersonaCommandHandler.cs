using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrearPersona.Context;
using CrearPersona.Models;
using MediatR;

namespace CrearPersona.Mediator
{
    public class CrearPersonaCommandHandler : IRequestHandler<CrearPersonaCommand, PersonaResponse>
    {
        private readonly ApplicationDbContext _context;
        public CrearPersonaCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PersonaResponse> Handle(CrearPersonaCommand request, CancellationToken cancellationToken)
        {
            var persona = new Persona
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                FechaNacimiento = request.FechaNacimiento,
                Identificacion = request.Identificacion,
                CiudadNacimiento = request.CiudadNacimiento,
                PaisNacimiento = request.PaisNacimiento
            };

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync(cancellationToken);

            return new PersonaResponse
            {
                Id = persona.Id,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                FechaNacimiento = persona.FechaNacimiento,
                Identificacion = persona.Identificacion,
                CiudadNacimiento = persona.CiudadNacimiento,
                PaisNacimiento = persona.PaisNacimiento,
                MensajeError = "Persona creada exitosamente"
            };
        }
    }
}
