using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Dominio.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeCalidad.Dominio.Delegados
{
    public delegate void LimiteInferiorDelegate(OrdenDeProduccion op, TipoDeDefecto tipo);
}
