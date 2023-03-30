using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;

namespace ControlDeCalidad.Aplicacion.Servicios
{
    public class GestionModeloService : IGestionModeloService
    {
        private readonly IRepository<Modelo> _repoModelos;

        public GestionModeloService(IRepository<Modelo> repoModelos)
        {
            _repoModelos = repoModelos;
        }

        public void CrearModelo(string sku, string denominacion,
            int limiteInferiorObservado, int limiteSuperiorObservado,
            int limiteInferiorReproceso, int limiteSuperiorReproceso)
        {
            // La validacion es mas rapida que el acceso a la DB
            // seria mejora validar antes
            ValidarDatos(
                sku, denominacion,
                limiteInferiorObservado, limiteSuperiorObservado,
                limiteInferiorReproceso, limiteSuperiorReproceso);

            Modelo? modeloRepositorio =
                _repoModelos.BuscarPor(m => m.SKU == sku).SingleOrDefault();

            if (modeloRepositorio == null)
            {
                Modelo nuevoModelo = new Modelo(
                    sku, denominacion,
                    limiteInferiorReproceso, limiteSuperiorReproceso,
                    limiteInferiorObservado, limiteSuperiorObservado
                );

                _repoModelos.Agregar(nuevoModelo);
                _repoModelos.UnidadDeTrabajo.Confirmar();
            }
        }

        public void ActualizarModelo(
            string sku, string denominacion, 
            int limiteInferiorObservado, int limiteSuperiorObservado, 
            int limiteInferiorReproceso, int limiteSuperiorReproceso)
        {
            ValidarDatos(
                sku, denominacion,
                limiteInferiorObservado, limiteSuperiorObservado,
                limiteInferiorReproceso, limiteSuperiorReproceso);
            
            Modelo? modelo =
                _repoModelos.BuscarPor(m => m.SKU == sku).SingleOrDefault();

            if (modelo != null)
            {
                modelo.Denominacion = denominacion;
                modelo.LimiteInferiorReproceso = limiteInferiorReproceso;
                modelo.LimiteSuperiorReproceso = limiteSuperiorReproceso;
                modelo.LimiteInferiorObservado = limiteInferiorObservado;
                modelo.LimiteSuperiorObservado = limiteSuperiorObservado;

                _repoModelos.Modificar(modelo);
                _repoModelos.UnidadDeTrabajo.Confirmar();
            }
        }

        public void EliminarModelo(string sku)
        {
            Modelo? modelo = _repoModelos.BuscarPor(m => m.SKU == sku).SingleOrDefault();

            if (modelo != null)
            {
                _repoModelos.Eliminar(modelo);
                _repoModelos.UnidadDeTrabajo.Confirmar();
            }
        }

        private void ValidarDatos(
            string sku, string denominacion,
            int limiteInferiorObservado, int limiteSuperiorObservado,
            int limiteInferiorReproceso, int limiteSuperiorReproceso)
        {
            bool limitesObservadosValidos =
                    ValidarLimites(limiteInferiorReproceso, limiteSuperiorReproceso);

            bool limitesReprocesoValidos =
                ValidarLimites(limiteInferiorObservado, limiteSuperiorObservado);

            if (!limitesReprocesoValidos || !limitesObservadosValidos)
            {
                // Error de limites
                throw new Exception("Error de limites.");
            }
            if (string.IsNullOrEmpty(sku) || string.IsNullOrEmpty(denominacion))
            {
                // Error de datos
                throw new Exception("Error de datos");
            }
        }

        private bool ValidarLimites(int limiteInferior, int limiteSuperior)
        {
            bool sonPositivos = (limiteInferior * limiteSuperior) > 0;
            bool superiorMasAlto = limiteInferior < limiteSuperior;
            return sonPositivos && superiorMasAlto;
        }
    }
}
