using ControlDeCalidad.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeCalidad.Aplicacion.Contratos
{
    public interface IGestionColorService
    {
        void CrearColor(ColorDTO color);
        void ActualizarColor(ColorDTO color);
        void EliminarColor(string codigo);
    }
}
