using ControlDeCalidad.Dominio.EntidadesBase;

namespace ControlDeCalidad.Dominio.Entidades
{
    public class LineaDeTrabajo : EntidadPersistible
    {
        public LineaDeTrabajo() { }

        public LineaDeTrabajo(int numero)
        {
            Numero = numero;
        }

        public int Numero { get; set; }
    }
}
