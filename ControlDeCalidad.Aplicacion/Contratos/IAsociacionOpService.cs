using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeCalidad.Aplicacion.Contratos
{
    public interface IAsociacionOpService
    {
        void AsociarAOp(string token, string numOp);
        void DesasociarAOp(string numOp);
    }
}
