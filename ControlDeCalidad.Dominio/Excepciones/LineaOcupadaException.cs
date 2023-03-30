using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeCalidad.Dominio.Excepciones
{
    public class LineaOcupadaException : Exception
    {
        public override string Message => "Linea de Trabajo ocupada.";
    }
}
