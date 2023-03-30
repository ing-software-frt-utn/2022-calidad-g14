using ControlDeCalidad.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeCalidad.Aplicacion.Contratos
{
    public  interface IGestionOpService
    {
        OrdenDTO MiOp(string token);
        void PausarOp(string numero, string token);
        void ReanudarOp(string numero, string token);
        void FinalizarOp(string numero, string token);
    }
}
