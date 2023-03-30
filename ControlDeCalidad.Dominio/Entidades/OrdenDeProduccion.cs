using ControlDeCalidad.Dominio.Delegados;
using ControlDeCalidad.Dominio.EntidadesBase;
using ControlDeCalidad.Dominio.Enumeraciones;

namespace ControlDeCalidad.Dominio.Entidades
{
    public class OrdenDeProduccion : EntidadPersistible
    {
        public OrdenDeProduccion()
        {
            /*
             * EF llama a este constructor, donde los semaforos
             * todavia son nulos por el lazy loading y la forma 
             * de inicializar las entidades relacionadas a la op.
             * Sin la suscripcion a los semaforos, no puede haber alertas.
             * Deberia producirse la suscipcion a los semaforos y al handler
             * cada vez que se haga un registro.
             * Esto me suena a acomodar al dominio en base de las 
             * tecnologias a usar y no parece buena idea.
             */
            //SuscribirASemaforos();
        }

        public OrdenDeProduccion(
            string numero, 
            Color color, Modelo modelo, 
            Empleado supDeLinea, LineaDeTrabajo linea)
        {
            Numero = numero;
            Color = color;
            Modelo = modelo;
            SupervisorDeLinea = supDeLinea;
            Linea = linea;

            Iniciar();
        }

        public string Numero { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }

        public Color Color { get; set; }
        public Modelo Modelo { get; set; }
        public EstadoOP Estado { get; set; }

        public Empleado SupervisorDeLinea {get;set;}
        public LineaDeTrabajo Linea { get; set; }

        public List<Jornada> Jornadas { get; set; }
        public Jornada? JornadaActiva => GetJornadaActiva();

        // Limites y semaforo
        public Semaforo? SemaforoObservado { get; set; }
        public Semaforo? SemaforoReproceso { get; set; }

        public event LimiteInferiorDelegate LimiteInferior;
        public event LimiteSuperiorDelegate LimiteSuperior;     

        public Jornada? GetJornadaActiva()
        {
            Jornada? jornadaActiva = 
                Jornadas.Where(j => j.FechaFin == null).SingleOrDefault();
            return jornadaActiva;
        }

        #region Asociacion de Supervisor de Calidad
        public void AsociarSupervisorDeCalidad(Empleado supCalidad, Turno turno)
        {
            // Supervisor de calidad debe tener una sola OP
            // No debe haber una jornada activa con otro supervisor 
            if(JornadaActiva == null && supCalidad.Puesto == PuestoDeTrabajo.SupervisorDeCalidad)
            {
                Jornada jornada = new Jornada(supCalidad, turno);
                Jornadas.Add(jornada);
            }
            else
            {
                throw new Exception("No se pudo asociar a la orden.");
            }
        }

        public void DesasociarSupervisorDeCalidad()
        {
            if(JornadaActiva != null)
            {
                JornadaActiva.FechaFin = DateTime.Now;
            }
        }
        #endregion Asociacion de Supervisor de Calidad

        #region Cambio de Estado
        // Considerar esto porque el adaptador de DTO crea una OP
        // No tiene sentido que se inicie cuando aun se este verificando
        public void Iniciar()
        {
            FechaInicio = DateTime.Now;
            Jornadas = new List<Jornada>();

            SemaforoObservado = 
                new Semaforo(Modelo.LimiteInferiorObservado, Modelo.LimiteSuperiorObservado, TipoDeDefecto.Observado);
            SemaforoReproceso = 
                new Semaforo(Modelo.LimiteInferiorReproceso, Modelo.LimiteSuperiorReproceso, TipoDeDefecto.Reproceso);
            SuscribirASemaforos();

            Estado = EstadoOP.Iniciada;
        }

        public void Reanudar()
        {
            if(Estado == EstadoOP.Pausada)
            {
                Estado = EstadoOP.Iniciada;
                // Revisar estado del semaforo para alertar nuevamente
                // en una jornada nueva, por ejemplo:
                SemaforoObservado.VerificarEstado();
                SemaforoReproceso.VerificarEstado();
            }
        }

        public void Pausar()
        {
            if(Estado == EstadoOP.Iniciada)
            {
                Estado = EstadoOP.Pausada;
                SemaforoObservado.ReiniciarContador();
                SemaforoReproceso.ReiniciarContador();
            }
        }

        public void Finalizar()
        {
            if(Estado != EstadoOP.Finalizada)
            {
                Estado = EstadoOP.Finalizada;
                FechaFinalizacion = DateTime.Now;
                DesasociarSupervisorDeCalidad();
            }
        }
        #endregion Cambio de Estado

        #region Registro de incidencias
        public void RegistrarParDePrimera(short valor, short horaPlanilla, DateTime horaReal)
        {
            if(Estado == EstadoOP.Iniciada)
            {
                JornadaActiva?.RegistrarParDePrimera(valor, horaPlanilla, horaReal);
            }
            else
            {
                throw new Exception("La Orden de Produccion no esta activa.");
            }
        }

        public void RegistrarDefecto(
            short valor, Pie pie, Defecto infoDefecto, short horaPlanilla, DateTime horaReal)
        {
            if (Estado == EstadoOP.Iniciada)
            {
                SuscribirASemaforos(); // Se podria pasar los metodos del handler directamente los eventos del semaforo
                JornadaActiva?.RegistrarDefecto(valor, pie, infoDefecto, horaPlanilla, horaReal);
                ActualizarSemaforos(valor, infoDefecto);
            }
            else
            {
                throw new Exception("La Orden de Produccion no esta activa.");
            }
        }

        private void ActualizarSemaforos(short valor, Defecto infoDefecto)
        {
            if(infoDefecto.Tipo == TipoDeDefecto.Observado)
                SemaforoObservado.ActualizarContador(valor);
            else
                SemaforoReproceso.ActualizarContador(valor);
        }
        #endregion Registro de incidencias

        #region Semaforo
        private void SuscribirASemaforos()
        {
            SemaforoObservado.LimiteInferiorEvent += OnLimiteInferior;
            SemaforoObservado.LimiteSuperiorEvent += OnLimiteSuperior;
            SemaforoReproceso.LimiteInferiorEvent += OnLimiteInferior;
            SemaforoReproceso.LimiteSuperiorEvent += OnLimiteSuperior;
        }

        private void OnLimiteInferior(object sender, TipoDeDefecto tipoDeDefecto)
        {
            LimiteInferior?.Invoke(this, tipoDeDefecto);
        }

        private void OnLimiteSuperior(object sender, TipoDeDefecto tipoDeDefecto)
        {
            LimiteSuperior?.Invoke(this, tipoDeDefecto);
            // Pausar();
        }

        public void ReiniciarSemaforo(TipoDeDefecto tipoDeDefecto)
        {
            SemaforoObservado.ReiniciarContador();
            SemaforoReproceso.ReiniciarContador();
        }
        #endregion Semaforo
    }
}
