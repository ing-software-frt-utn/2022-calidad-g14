using ControlDeCalidad.Dominio.EntidadesBase;
using ControlDeCalidad.Dominio.Enumeraciones;

namespace ControlDeCalidad.Dominio.Entidades
{
    public class Empleado : EntidadPersistible
    {
        public Empleado() { }

        public Empleado(int dni, string nombre, string apellido, string correo, PuestoDeTrabajo puesto)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            CorreoElectronico = correo;
            Puesto = puesto;
        }

        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }

        public PuestoDeTrabajo Puesto { get; set; }

        // Era private set antes de usar EF
        // Seria mejor configurar la FK en el ModelBuilder
        // pero por algun motivo no anda
        //public int OpAsociadaId { get; set; }
        //public OrdenDeProduccion OpAsociada { get; set; }
        //public void AsociarOrdenDeProduccion(OrdenDeProduccion op)
        //{
        //    if(OpAsociada == null)
        //    {
        //        OpAsociada = op;
        //    }
        //}
        
        //public void DesasociarOrdenDeProduccion()
        //{
        //    if (OpAsociada != null)
        //    {
        //        OpAsociada = null;
        //    }
        //}
    }
}
