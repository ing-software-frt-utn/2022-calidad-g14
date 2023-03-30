using ControlDeCalidad.Dominio.Delegados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeCalidad.Aplicacion.Contratos
{
    public interface IAlertasService
    {
        void SuscribirASemaforo(string numOp, LimiteInferiorDelegate handlerInferior, LimiteSuperiorDelegate handlerSuperior);
    }
}
