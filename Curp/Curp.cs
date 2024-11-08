using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Curp.Modelos;

namespace Curp
{
    public class Curp : ICurp
    {
        private readonly List<Persona> _personas;
        public Curp(List<Persona> listaPersonas) {
            _personas = listaPersonas;
        }
        public Persona obtenerPersonaConCurp(String curp)
        {
            if (curp == null)
            {
                throw new ArgumentException("Te falta completar algún campo requerido. Por favor verifica.");
            }
            else if (!Regex.IsMatch(curp, "[A-Z]{4}\\d{6}[HMX]{1}[A-Z]{5}[\\dA-Z]{1}\\d"))
            {
                throw new ArgumentException("Algún campo es inválido. Por favor verifica.");
            } else if (!curpExiste(curp))
            {
                throw new ArgumentException("No hay ninguna persona registrada con ese curp.");
            }
            var persona = _personas.FirstOrDefault(p => p.Curp == curp);

            if (persona != null && !persona.TaBibito)
            {
                throw new ArgumentException("Esta CURP fue dada de baja por defunción.");
            }
            return persona;

        }

        public bool curpExiste(String curp)
        {
            return _personas.Any(p => p.Curp == curp);
        }
    }
}
