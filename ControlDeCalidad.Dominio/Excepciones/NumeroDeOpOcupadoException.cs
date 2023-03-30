using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeCalidad.Dominio.Excepciones
{
    public class NumeroDeOpOcupadoException : Exception
    {
        override public string Message => "Numero de OP ocupado";
    }
}
