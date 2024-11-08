using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curp.Modelos
{
    public class Persona
    {
        public string Curp { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Sexo { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string EntidadNacimiento { get; set; }
        public bool TaBibito { get; set; }

    }
}
