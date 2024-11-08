using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curp.Modelos;

namespace Curp
{
    public interface ICurp
    {
        public bool curpExiste(String curp);
        public Persona obtenerPersonaConCurp(String curp);
    }
}
