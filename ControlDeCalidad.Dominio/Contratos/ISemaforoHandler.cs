using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Dominio.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeCalidad.Dominio.Contratos
{
    public interface ISemaforoHandler
    {
        void OnAlertaLimiteInferior(OrdenDeProduccion op, TipoDeDefecto tipo);
        void OnAlertaLimiteSuperior(OrdenDeProduccion op, TipoDeDefecto tipo);
    }
}
