using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Dominio.Enumeraciones;
using System.ComponentModel.DataAnnotations;

namespace ControlDeCalidad.Aplicacion.DTOs
{
    public class OrdenDTO
    {
        public OrdenDTO()
        {

        }

        public OrdenDTO(OrdenDeProduccion op)
        {
            Numero = op.Numero;
            if(op.Color != null)
                Color = new ColorDTO(op.Color);
            if(op.Modelo != null)
                Modelo = new ModeloDTO(op.Modelo);
            if (op.SupervisorDeLinea != null)
                SupervisorDeLinea = new EmpleadoDTO(op.SupervisorDeLinea);
            if (op.Linea != null)
                Linea = new LineaDeTrabajoDTO(op.Linea);
            Estado = op.Estado.ToString();
        }

        // Hay campos que no se requieren para la creacion
        // pero si para la consulta. Usar distintos DTOs?
        [Required] public string Numero { get; set; }

        // Al no transferir el DTO de color y modelo, se expone la id.
        // Quizas sea mejor solo pasar los campos relevantes de cada uno?
        [Required] public ColorDTO Color { get; set; }
        [Required] public ModeloDTO Modelo { get; set; }

        [Required] public EmpleadoDTO SupervisorDeLinea { get; set; }
        [Required] public LineaDeTrabajoDTO Linea { get; set; }

        [Required] public string Estado { get; set; }

        //public static OrdenDeProduccion DTOAOrden(OrdenDTO dto)
        //{
        //    Color color = ColorDTO.DTOAColor(dto.Color);
        //    Modelo modelo = ModeloDTO.DTOAModelo(dto.Modelo);
        //    Empleado supervisorDeLinea = EmpleadoDTO.DTOAEmpleado();
        //    LineaDeTrabajo linea = LineaDeTrabajoDTO.DTOALinea();
        //    return new OrdenDeProduccion(dto.Numero, color, dto.Modelo, dto.SupervisorDeLinea, dto.Linea);
        //}
    }
}
