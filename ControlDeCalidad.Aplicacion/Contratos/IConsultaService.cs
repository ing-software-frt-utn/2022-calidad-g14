using ControlDeCalidad.Dominio.EntidadesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeCalidad.Aplicacion.Contratos
{
    public interface IConsultaService<T> where T : EntidadPersistible
    {
        IEnumerable<T> ObtenerTodas();
        IEnumerable<T> BuscarPor(Expression<Func<T, bool>> filtro);
    }
}
