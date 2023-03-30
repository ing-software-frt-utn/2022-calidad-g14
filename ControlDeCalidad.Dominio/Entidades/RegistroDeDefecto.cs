using ControlDeCalidad.Dominio.EntidadesBase;
using ControlDeCalidad.Dominio.Enumeraciones;

namespace ControlDeCalidad.Dominio.Entidades
{
    public class RegistroDeDefecto : EntidadPersistible
    {
        public RegistroDeDefecto() { }

        public RegistroDeDefecto(Pie pie, Defecto defecto, short valor, short hora, DateTime horaReal)
        {
            Pie = pie;
            Defecto = defecto;
            Valor = valor;

            HoraPlanilla = hora;
            HoraReal = horaReal;
        }

        public short HoraPlanilla { get; set; }
        public DateTime HoraReal { get; set; }
       
        public Pie Pie { get; set; }
        public Defecto Defecto { get; set; }
        public short Valor { get; set; }
    }
}
