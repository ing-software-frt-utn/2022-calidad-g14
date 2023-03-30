using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeCalidad.Aplicacion.Contratos
{
    public  interface IGestionModeloService
    {
        void CrearModelo(
            string sku, string denominacion,
            int limiteInferiorObservado, int limiteSuperiorObservado,
            int limiteInferiorReproceso, int limiteSuperiorReproceso);
        void ActualizarModelo(
            string sku, string denominacion,
            int limiteInferiorObservado, int limiteSuperiorObservado,
            int limiteInferiorReproceso, int limiteSuperiorReproceso);
        void EliminarModelo(string sku);

    }
}
