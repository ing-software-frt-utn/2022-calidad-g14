using ControlDeCalidad.Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace ControlDeCalidad.Aplicacion.DTOs
{
    public class ColorDTO
    {
        public ColorDTO() { }

        public ColorDTO(Color color)
        {
            Codigo = color.Codigo;  
            Descripcion = color.Descripcion;
        }

        [Required] public string? Codigo { get; set; }
        [Required] public string? Descripcion { get; set; }

        //Aqui o en una clase Adaptador? Es el unico DTO que usa adaptacion inversa
        public static Color DTOAColor(ColorDTO dto)
        {
            return new Color
            (
                codigo: dto.Codigo ?? string.Empty,
                descripcion: dto.Descripcion ?? string.Empty
            );
        }
    }
}
