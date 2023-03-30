using ControlDeCalidad.AccesoADatos.Contratos;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControlDeCalidad.AccesoADatos.Repositorios
{
    public class OrdenDeProduccionRepository : Repository<OrdenDeProduccion>, IOrdenDeProduccionRepository
    {
        public OrdenDeProduccionRepository(IUnidadDeTrabajoEF unidadDeTrabajo) : base(unidadDeTrabajo)
        {

        }

        public OrdenDeProduccion BuscarConJornadaActiva(Expression<Func<OrdenDeProduccion, bool>> filtro)
        {
            // Trae todas las inspecciones
            var op = GetSet()
                    .Include(op => op.SemaforoObservado)
                    .Include(op => op.SemaforoReproceso)
                    .Include(op => op.Linea)
                    .Include(op => op.Jornadas.Where(j => j.FechaFin == null))
                    .ThenInclude(j => j.RegistrosDeDefecto)
                    .ThenInclude(rd => rd.Defecto)
                    .Include(op => op.Jornadas.Where(j => j.FechaFin == null))
                    .ThenInclude(j => j.ParesDePrimera)
                    .Include(op => op.Jornadas.Where(j => j.FechaFin == null))
                    .ThenInclude(j => j.SupervisorDeCalidad)
                    .Include(op => op.Jornadas.Where(j => j.FechaFin == null))
                    .ThenInclude(j => j.Turno)
                    .Where(filtro.Compile())
                    .Single();
            return op;
        }

        public override IEnumerable<OrdenDeProduccion> ObtenerTodos()
        {
            var ops = GetSet()
                    .Include(op => op.Modelo)
                    .Include (op => op.Color)
                    .Include(op => op.Linea)
                    .Include(j => j.SupervisorDeLinea)
                    .Include(op => op.SemaforoObservado)
                    .Include(op => op.SemaforoReproceso);
            return ops;
        }
    }
}
