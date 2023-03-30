using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Aplicacion.DTOs;
using ControlDeCalidad.Aplicacion.Sesiones;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Dominio.Enumeraciones;

namespace ControlDeCalidad.Aplicacion.Servicios
{
    public class GestionOpService : IGestionOpService
    {
        private IRepository<OrdenDeProduccion> _repoOrdenes;
        public GestionOpService(
            IRepository<OrdenDeProduccion> repoOrdenes)
        {
            _repoOrdenes = repoOrdenes;
        }

        public OrdenDTO MiOp(string token)
        {
            string unescapedToken = Uri.UnescapeDataString(token);
            int dniSupervisor = AdministradorDeSesiones.Instancia.ObtenerDatos(unescapedToken).DNI;

            OrdenDeProduccion? opAsociada =
                _repoOrdenes.BuscarPor(
                    op => op.SupervisorDeLinea.DNI == dniSupervisor && op.Estado != EstadoOP.Finalizada)
                .SingleOrDefault();

            if (opAsociada == null)
                throw new Exception("No tiene una orden de produccion asociada.");

            _repoOrdenes.ComponerReferencia(opAsociada, opAsociada => opAsociada.Modelo);
            _repoOrdenes.ComponerReferencia(opAsociada, opAsociada => opAsociada.Color);
            _repoOrdenes.ComponerReferencia(opAsociada, opAsociada => opAsociada.Linea);
            _repoOrdenes.ComponerReferencia(opAsociada, opAsociada => opAsociada.SupervisorDeLinea);

            return new OrdenDTO(opAsociada);
        }

        public void FinalizarOp(string numero, string token)
        {
            OrdenDeProduccion? opAsociada = BuscarOpAsociada(numero, token);

            if(opAsociada != null)
            {
                opAsociada.Finalizar();
                _repoOrdenes.Modificar(opAsociada);
                _repoOrdenes.UnidadDeTrabajo.Confirmar();
            }
            else
            {
                throw new Exception("Numero de OP no asociado al Supervisor de Linea");
            }
        }

        public void PausarOp(string numero, string token)
        {
            OrdenDeProduccion? opAsociada = BuscarOpAsociada(numero, token);         

            if (opAsociada != null)
            {
                opAsociada.Pausar();
                _repoOrdenes.Modificar(opAsociada);
                _repoOrdenes.UnidadDeTrabajo.Confirmar();
            }
            else
            {
                throw new Exception("Numero de OP no asociado al Supervisor de Linea");
            }
        }

        public void ReanudarOp(string numero, string token)
        {
            OrdenDeProduccion? opAsociada = BuscarOpAsociada(numero, token);

            if (opAsociada != null)
            {
                // Para la verificacion de semaforos
                _repoOrdenes.ComponerReferencia(opAsociada, opAsociada => opAsociada.SemaforoObservado!);
                _repoOrdenes.ComponerReferencia(opAsociada, opAsociada => opAsociada.SemaforoReproceso!);
                opAsociada.Reanudar();
                _repoOrdenes.Modificar(opAsociada);
                _repoOrdenes.UnidadDeTrabajo.Confirmar();
            }
            else
            {
                throw new Exception("Numero de OP no asociado al Supervisor de Linea");
            }
        }

        private OrdenDeProduccion? BuscarOpAsociada(string numero, string token)
        {
            string unescapedToken = Uri.UnescapeDataString(token);
            int dniSupervisor = AdministradorDeSesiones.Instancia.ObtenerDatos(unescapedToken).DNI;

            OrdenDeProduccion? opAsociada =
                _repoOrdenes.BuscarPor(
                    op => op.SupervisorDeLinea.DNI == dniSupervisor && op.Numero == numero)
                .SingleOrDefault();

            return opAsociada;
        }
    }
}
