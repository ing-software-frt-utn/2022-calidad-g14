using ControlDeCalidad.Dominio.Entidades;
using System.Linq.Expressions;

namespace ControlDeCalidad.Dominio.Contratos
{
    public interface IOrdenDeProduccionRepository : IRepository<OrdenDeProduccion>
    {
        OrdenDeProduccion BuscarConJornadaActiva(Expression<Func<OrdenDeProduccion, bool>> filtro);
    }
}