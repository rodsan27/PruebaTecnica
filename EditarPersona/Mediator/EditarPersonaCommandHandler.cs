using EditarPersona.Context;
using EditarPersona.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditarPersona.Mediator
{
    public class EditarPersonaCommandHandler : IRequestHandler<EditarPersonaCommand, PersonaResponse>
    {
        private readonly ApplicationDbContext _context;

        public EditarPersonaCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PersonaResponse> Handle(EditarPersonaCommand request, CancellationToken cancellationToken)
        {
            // Buscar la persona en la base de datos
            var persona = await _context.Personas.FindAsync(request.Id);
            if (persona == null)
            {
                return new PersonaResponse
                {
                    MensajeError = "Persona no encontrada."
                };
            }

            // Actualizar los datos
            persona.Nombre = request.Nombre;
            persona.Apellido = request.Apellido;
            persona.FechaNacimiento = request.FechaNacimiento;
            persona.Identificacion = request.Identificacion;
            persona.CiudadNacimiento = request.CiudadNacimiento;
            persona.PaisNacimiento = request.PaisNacimiento;

            // Guardar cambios en la base de datos
            await _context.SaveChangesAsync(cancellationToken);

            // Retornar los datos actualizados
            return new PersonaResponse
            {
                Id = persona.Id,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                FechaNacimiento = persona.FechaNacimiento.ToString("yyyy-MM-dd"),
                Identificacion = persona.Identificacion,
                CiudadNacimiento = persona.CiudadNacimiento,
                PaisNacimiento = persona.PaisNacimiento,
                MensajeError = "Persona actualizada exitosamente"
            };
        }
    }
}
