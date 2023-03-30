using ControlDeCalidad.Dominio.Entidades;

namespace ControlDeCalidad.Aplicacion.DTOs
{
    public class LineaDeTrabajoDTO
    {
        public LineaDeTrabajoDTO(LineaDeTrabajo linea)
        {
            Numero = linea.Numero;
        }

        public int Numero { get; set; }
    }
}
