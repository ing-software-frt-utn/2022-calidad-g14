using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.EntidadesBase;
using System.Linq.Expressions;

namespace ControlDeCalidad.Aplicacion.Servicios
{
    public class ConsultaService<T> : IConsultaService<T> where T : EntidadPersistible
    {
        private IRepository<T> _repository;
        public ConsultaService(IRepository<T> repo)
        {
            _repository = repo;
        }

        public IEnumerable<T> BuscarPor(Expression<Func<T, bool>> filtro)
        {
            return _repository.BuscarPor(filtro);
        }

        public IEnumerable<T> ObtenerTodas()
        {
            return _repository.ObtenerTodos();
        }
    }
}
