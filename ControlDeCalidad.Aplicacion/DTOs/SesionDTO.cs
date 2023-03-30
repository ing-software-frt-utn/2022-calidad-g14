using ControlDeCalidad.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeCalidad.Aplicacion.DTOs
{
    public class SesionDTO
    {
        public SesionDTO(string token, EmpleadoDTO empleado)
        {
            Token = token;
            Empleado = empleado;
        }

        public string Token { get; set; }
        public EmpleadoDTO Empleado { get; set;}
    }
}
