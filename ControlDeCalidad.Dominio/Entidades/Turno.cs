using ControlDeCalidad.Dominio.EntidadesBase;

namespace ControlDeCalidad.Dominio.Entidades
{
    public class Turno : EntidadPersistible
    {
        public Turno()
        {

        }

        // No acepta horas que no sean redondas
        public Turno(int horaInicio, int horaFin, string descripcion)
        {
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Descripcion = descripcion;
        }

        public int HoraInicio { get; set; }
        public int HoraFin { get; set; }

        //public DateTime HoraInicio { get; set; }
        //public DateTime HoraFin { get; set; }

        public string Descripcion { get; set; }

        public bool DentroDeTurno(DateTime hora)
        {
            TimeOnly horaInicio = new TimeOnly(HoraInicio, 0);
            TimeOnly horaFin = new TimeOnly(HoraFin, 0);
            return TimeOnly.FromDateTime(hora).IsBetween(horaInicio, horaFin);
        }
    }
}
