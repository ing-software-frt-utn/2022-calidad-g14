using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Aplicacion.Sesiones;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Dominio.Enumeraciones;

namespace ControlDeCalidad.Aplicacion.Servicios
{
    public class AsociacionOpService : IAsociacionOpService
    {
        private IRepository<OrdenDeProduccion> _ordenesRepo;
        private IRepository<Turno> _turnosRepo;
        public AsociacionOpService(
            IRepository<OrdenDeProduccion> ordenesRepo,
            IRepository<Turno> turnosRepo) 
        {
            _ordenesRepo = ordenesRepo;
            _turnosRepo = turnosRepo;
        }

        public void AsociarAOp(string token, string numOp)
        {
            string unscapedToken = Uri.UnescapeDataString(token);
            Empleado supCalidad = AdministradorDeSesiones.Instancia.ObtenerDatos(unscapedToken);
            if (supCalidad.Puesto != PuestoDeTrabajo.SupervisorDeCalidad)
                throw new Exception("Solo un supervisor de calidad puede asociarse.");

            DateTime horaActual = DateTime.Now;
            IEnumerable<Turno> turnos = _turnosRepo.ObtenerTodos();
            Turno turnoActual = turnos.Where(t => t.DentroDeTurno(DateTime.Now)).Single();
            OrdenDeProduccion? op = _ordenesRepo.BuscarPor(op => op.Numero == numOp).SingleOrDefault();
            if (op != null)
            {
                _ordenesRepo.ComponerColeccion(op, op => op.Jornadas);
                op.AsociarSupervisorDeCalidad(supCalidad, turnoActual);
                _ordenesRepo.Modificar(op);
                _ordenesRepo.UnidadDeTrabajo.Confirmar();
            }
        }

        public void DesasociarAOp(string numOp)
        {
            OrdenDeProduccion? op = _ordenesRepo.BuscarPor(op => op.Numero == numOp).SingleOrDefault();
            if (op != null)
            {
                _ordenesRepo.ComponerColeccion(op, op => op.Jornadas);
                op.DesasociarSupervisorDeCalidad();
                _ordenesRepo.Modificar(op);
                _ordenesRepo.UnidadDeTrabajo.Confirmar();
            }
        }
    }
}
