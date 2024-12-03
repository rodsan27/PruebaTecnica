using ConsultarPersona.Context;
using ConsultarPersona.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultarPersona.Mediator
{
    public class ConsultarPersonaQueryHandler : IRequestHandler<ConsultarPersonaQuery, List<PersonaResponse>>
    {
        private readonly ApplicationDbContext _context;

        public ConsultarPersonaQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PersonaResponse>> Handle(ConsultarPersonaQuery request, CancellationToken cancellationToken)
        {
            // Consultar todas las personas y mapearlas a PersonaResponse
            return await Task.Run(() => _context.Personas.Select(p => new PersonaResponse
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                FechaNacimiento = p.FechaNacimiento,
                Identificacion = p.Identificacion,
                CiudadNacimiento = p.CiudadNacimiento,
                PaisNacimiento = p.PaisNacimiento
            }).ToList(), cancellationToken);
        }
    }
}
