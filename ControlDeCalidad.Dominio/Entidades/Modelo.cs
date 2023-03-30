using ControlDeCalidad.Dominio.EntidadesBase;

namespace ControlDeCalidad.Dominio.Entidades
{
    public class Modelo : EntidadPersistible
    {
        public Modelo() { }

        public Modelo(string sku, string denominacion,
            int limiteInferiorReproceso, int limiteSuperiorReproceso, 
            int limiteInferiorObservado, int limiteSuperiorObservado)
        {
            SKU = sku;
            Denominacion = denominacion;
            LimiteInferiorReproceso = limiteInferiorReproceso;
            LimiteSuperiorReproceso = limiteSuperiorReproceso;
            LimiteInferiorObservado = limiteInferiorObservado;
            LimiteSuperiorObservado = limiteSuperiorObservado;
        }

        public string SKU { get; set; } // Formato?
        public string Denominacion { get; set; }

        public int LimiteInferiorReproceso { get; set; }
        public int LimiteSuperiorReproceso { get; set; }
        public int LimiteInferiorObservado { get; set; }
        public int LimiteSuperiorObservado { get; set; }
    }
}
