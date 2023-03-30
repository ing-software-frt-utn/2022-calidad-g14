using ControlDeCalidad.AccesoADatos.Contratos;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.EntidadesBase;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControlDeCalidad.AccesoADatos.Repositorios
{
    public class Repository<T> : IRepository<T> where T : EntidadPersistible
    {
        private readonly IUnidadDeTrabajoEF _unidadDeTrabajo;

        public Repository(IUnidadDeTrabajoEF unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo ?? throw new ArgumentNullException("unitOfWork");
        }

        public IUnidadDeTrabajo UnidadDeTrabajo => _unidadDeTrabajo;

        protected DbSet<T> GetSet()
        {
            return _unidadDeTrabajo.CrearSet<T>();
        }

        public void Agregar(T item)
        {
            if (item != null)
            {
                GetSet().Add(item);
            }
        }

        public void Eliminar(T item)
        {
            if (item != null)
            {
                GetSet().Remove(item);
            }
        }

        public void Eliminar(int id)
        {
            T item = BuscarPorId(id);
            if (item != null)
            {
                GetSet().Remove(item);
            }
        }

        public void Modificar(T item)
        {
            if (item != null)
            {
                _unidadDeTrabajo.SetModificado(item);
            }
        }

        public virtual T BuscarPorId(int id)
        {
            return id != 0 ? GetSet().Find(id) : null;
        }

        public virtual IEnumerable<T> ObtenerTodos()
        {
            return GetSet();
        }

        public virtual IEnumerable<T> BuscarPor(Expression<Func<T, bool>> filtro)
        {
            return GetSet().Where(filtro);
        }

        public void DescartarCambios()
        {
            _unidadDeTrabajo.RevertirCambios();
        }

        public void Refrescar(T item)
        {
            if (item != null)
            {
                _unidadDeTrabajo.Refrescar(item);
            }
        }

        public void Refrescar(EntidadPersistible item)
        {
            if (item != null)
            {
                _unidadDeTrabajo.Refrescar(item);
            }
        }

        public T GetUnico(Expression<Func<T, bool>> filtro)
        {
            return GetSet().FirstOrDefault(filtro);
        }

        public void ComponerReferencia<T, E>(T item, Expression<Func<T, E>> entidad)
            where T : class
            where E : class
        {
            if(item != null)
                _unidadDeTrabajo.RelacionarEntidad(item, entidad);
        }

        public void ComponerColeccion<T, C>(T item, Expression<Func<T, IEnumerable<C>>> coleccion)
            where T : class
            where C : class
        {
            if (item != null)
                _unidadDeTrabajo.RelacionarColeccion(item, coleccion);
        }

        #region IDisposable 
        private bool _disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    UnidadDeTrabajo?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable
    }
}