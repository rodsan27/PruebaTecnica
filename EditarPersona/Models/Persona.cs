using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditarPersona.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FechaNacimiento { get; set; }
        public string Identificacion { get; set; }
        public string CiudadNacimiento { get; set; }
        public string PaisNacimiento { get; set; }
        public string database { get; set; }
        public string server { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
    }
}
