using System.Linq.Expressions;

namespace ControlDeCalidad.Dominio.Contratos
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        void Confirmar();
        void RevertirCambios();
        void RelacionarEntidad<T, E>(T item, Expression<Func<T, E>> entidad)
            where T : class
            where E : class;
        void RelacionarColeccion<T, C>(T item, Expression<Func<T, IEnumerable<C>>> coleccion)
            where T : class
            where C : class;
    }
}
