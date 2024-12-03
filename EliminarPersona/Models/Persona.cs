using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliminarPersona.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string database { get; set; }
        public string server { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
    }
}
