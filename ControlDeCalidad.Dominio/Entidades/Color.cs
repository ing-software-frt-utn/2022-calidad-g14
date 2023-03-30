using ControlDeCalidad.Dominio.EntidadesBase;

namespace ControlDeCalidad.Dominio.Entidades
{
    public class Color : EntidadPersistible
    {
        public Color() { }

        public Color(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }

        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
