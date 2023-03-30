using ControlDeCalidad.Dominio.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ControlDeCalidad.AccesoADatos.Contratos
{
    public interface IUnidadDeTrabajoEF : IUnidadDeTrabajo
    {
        DbSet<T> CrearSet<T>() where T : class;
        void SetModificado<T>(T item) where T : class;
        void Refrescar<T>(T item) where T : class;
    }
}
