using ControlDeCalidad.Dominio.EntidadesBase;
using ControlDeCalidad.Dominio.Enumeraciones;

namespace ControlDeCalidad.Dominio.Entidades
{
    public class Defecto : EntidadPersistible
    {
        public Defecto() { }

        public Defecto(string codigo, string descripcion, TipoDeDefecto tipo)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            Tipo = tipo;
        }

        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public TipoDeDefecto Tipo { get; set; }
    }
}
