﻿using EliminarPersona.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliminarPersona.Mediator
{
    public class EliminarPersonaCommand : IRequest<PersonaResponse>
    {
        public int Id { get; set; }
    }
}