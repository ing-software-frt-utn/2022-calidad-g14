using ControlDeCalidad.Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace ControlDeCalidad.Aplicacion.DTOs
{
    public class ModeloDTO
    {
        public ModeloDTO() { }

        public ModeloDTO(Modelo modelo)
        {
            SKU = modelo.SKU;
            Denominacion = modelo.Denominacion;
            LimiteInferiorReproceso = modelo.LimiteInferiorReproceso;
            LimiteSuperiorReproceso = modelo.LimiteSuperiorReproceso;
            LimiteInferiorObservado = modelo.LimiteInferiorObservado;
            LimiteSuperiorObservado = modelo.LimiteSuperiorObservado;
        }

        [Required] public string? SKU { get; set; }
        [Required] public string? Denominacion { get; set; }
        [Required] public int LimiteInferiorReproceso { get; set; }
        [Required] public int LimiteSuperiorReproceso { get; set; }
        [Required] public int LimiteInferiorObservado { get; set; }
        [Required] public int LimiteSuperiorObservado { get; set; }

        //public static Modelo DTOAModelo(ModeloDTO dto)
        //{
        //    return new Modelo
        //    (
        //        sku: dto.SKU ?? string.Empty,
        //        denominacion: dto.Denominacion ?? string.Empty,
        //        limiteInferiorReproceso: dto.LimiteInferiorReproceso,
        //        limiteSuperiorReproceso: dto.LimiteSuperiorReproceso,
        //        limiteInferiorObservado: dto.LimiteInferiorObservado,
        //        limiteSuperiorObservado: dto.LimiteSuperiorObservado
        //    );
        //}
    }
}
