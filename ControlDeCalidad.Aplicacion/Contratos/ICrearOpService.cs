using ControlDeCalidad.Dominio.Delegados;
using ControlDeCalidad.Dominio.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeCalidad.Aplicacion.Contratos
{
    public interface ICrearOpService
    {
        public void CrearOp(string numOP, string sku, string codColor, int numLinea, string token);
            //, LimiteInferiorDelegate delegadoLimiteInferior, LimiteSuperiorDelegate delegadoLimiteSuperior);
    }
}
