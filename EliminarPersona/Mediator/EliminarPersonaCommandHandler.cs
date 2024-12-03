using EliminarPersona.Context;
using EliminarPersona.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliminarPersona.Mediator
{
    public class EliminarPersonaCommandHandler : IRequestHandler<EliminarPersonaCommand, PersonaResponse>
    {
        private readonly ApplicationDbContext _context;

        public EliminarPersonaCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PersonaResponse> Handle(EliminarPersonaCommand request, CancellationToken cancellationToken)
        {
            var persona = await _context.Personas.FindAsync(request.Id);
            if (persona == null)
            {
                return new PersonaResponse
                {
                    MensajeError = "Persona no encontrada."
                };
            }

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync(cancellationToken);

            return new PersonaResponse
            {
                MensajeError = "Persona eliminada exitosamente."
            };
        }
    }
}
