using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Aplicacion.DTOs;
using ControlDeCalidad.Aplicacion.Sesiones;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Dominio.Enumeraciones;

namespace ControlDeCalidad.Aplicacion.Servicios
{
    public class InspeccionService : IInspeccionService, IDisposable
    {
        // Se muestran los n defectos mas encontrados
        private const int CantidadDeDefectosTop = 5;

        private IRepository<Defecto> _defectoRepository;
        private ISemaforoHandler _semaforoHandler;
        private IOrdenDeProduccionRepository _ordenesRepository;

        public InspeccionService(
            IRepository<Defecto> defectoRepository,
            IOrdenDeProduccionRepository inspeccionRepository,
            ISemaforoHandler semaforoHandler)
        {
            _defectoRepository = defectoRepository;
            _ordenesRepository = inspeccionRepository;
            _semaforoHandler = semaforoHandler;
        }

        public void RegistrarParDePrimera(string numOp, string token, short valor, short hora)
        {
            string unescapedToken = Uri.UnescapeDataString(token);
            Empleado supCalidad = AdministradorDeSesiones.Instancia.ObtenerDatos(unescapedToken);

            OrdenDeProduccion op = _ordenesRepository.BuscarConJornadaActiva(op => op.Numero == numOp);
            Jornada? jornadaActiva = op?.JornadaActiva; // op podria ser nula y recien controlo abajo.
                                                        // Esta bien usado el operador '?'?

            if (op != null && jornadaActiva?.SupervisorDeCalidad.DNI == supCalidad.DNI)
            {
                op.RegistrarParDePrimera(valor, hora, DateTime.Now);
                _ordenesRepository.Modificar(op);
                _ordenesRepository.UnidadDeTrabajo.Confirmar();
            }
        }

        public SemaforoDTO RegistrarDefecto(string numOp, string token, string codigoDefecto, Pie pie, short valor, short hora)
        {
            string unescapedToken = Uri.UnescapeDataString(token);
            Empleado supCalidad = AdministradorDeSesiones.Instancia.ObtenerDatos(unescapedToken);

            OrdenDeProduccion op = _ordenesRepository.BuscarConJornadaActiva(op => op.Numero == numOp);
            Jornada? jornadaActiva = op?.JornadaActiva;

            if (op != null && jornadaActiva?.SupervisorDeCalidad.DNI == supCalidad.DNI)
            {
                Defecto defecto = _defectoRepository.BuscarPor(d => d.Codigo == codigoDefecto).Single();

                // Necesita de los semaforos
                op.LimiteInferior += _semaforoHandler.OnAlertaLimiteInferior;
                op.LimiteSuperior += _semaforoHandler.OnAlertaLimiteSuperior;
                op.RegistrarDefecto(valor, pie, defecto, hora, DateTime.Now);

                _ordenesRepository.Modificar(op);
                _ordenesRepository.UnidadDeTrabajo.Confirmar();

                return ObtenerDatosEnLinea(op);
            }
            else
            {
                throw new Exception("No se pudo registrar el defecto.");
            }
        }

        private SemaforoDTO ObtenerDatosEnLinea(OrdenDeProduccion op)
        {
            Jornada jornadaActiva = op.JornadaActiva!;

            // Obtener los 5 defectos de reproceso de observado en la ultima hora
            var topObservado = ObtenerTopDefectos(jornadaActiva, TipoDeDefecto.Observado);

            // Obtener los 5 defectos de reproceso de reproceso en la ultima hora
            var topReproceso = ObtenerTopDefectos(jornadaActiva, TipoDeDefecto.Reproceso);

            // Se crea el DTO y se envia
            return new SemaforoDTO()
            {
                Linea = op.Linea.Numero,
                ColorSemaforoObservado = op.SemaforoObservado!.Color.ToString(),
                ColorSemaforoReproceso = op.SemaforoReproceso!.Color.ToString(),
                TopObservado = topObservado, TopReproceso = topReproceso 
            };
        }

        public SemaforoDTO ObtenerDatosEnLinea(int numeroLinea)
        {
            OrdenDeProduccion op =
                _ordenesRepository.BuscarConJornadaActiva(op => op.Linea.Numero == numeroLinea);
            Jornada? jornadaActiva = op.JornadaActiva;

            if(jornadaActiva != null)
            {
                var topObservado = ObtenerTopDefectos(jornadaActiva, TipoDeDefecto.Observado);
                var topReproceso = ObtenerTopDefectos(jornadaActiva, TipoDeDefecto.Reproceso);

                return new SemaforoDTO()
                {
                    Linea = op.Linea.Numero,
                    ColorSemaforoObservado = op.SemaforoObservado!.Color.ToString(),
                    ColorSemaforoReproceso = op.SemaforoReproceso!.Color.ToString(),
                    TopObservado = topObservado,
                    TopReproceso = topReproceso
                };
            }
            else
            {
                throw new Exception("No hay una orden en la linea de trabajo.");
            }
            
        }

        private List<string> ObtenerTopDefectos(Jornada jornada, TipoDeDefecto tipo)
        {
            // Hay que componer el registro de defecto con su defecto, serian muchas consultas
            return jornada.RegistrosDeDefecto
                    .Where(rd =>
                        rd.Defecto.Tipo == tipo &&
                        rd.HoraReal > DateTime.Now.AddHours(-1))
                    .GroupBy(rd => rd.Defecto.Descripcion)
                    .OrderByDescending(g => g.Count())
                    .Take(CantidadDeDefectosTop)
                    .Select(rd => rd.Key)
                    .ToList();
        }

        #region IDisposable
        private bool _disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void IInspeccionService.Dispose()
        {
            Dispose();
        }
        #endregion IDisposable
    }
}
