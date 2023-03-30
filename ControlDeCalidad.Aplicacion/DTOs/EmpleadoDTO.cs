using ControlDeCalidad.Dominio.Entidades;

namespace ControlDeCalidad.Aplicacion.DTOs
{
    public class EmpleadoDTO
    {
        public EmpleadoDTO(Empleado empleado)
        {
            DNI = empleado.DNI;
            Nombre = empleado.Nombre;
            Apellido = empleado.Apellido;
            CorreoElectronico = empleado.CorreoElectronico;
        }

        public int DNI { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? CorreoElectronico { get; set; }
    }
}
