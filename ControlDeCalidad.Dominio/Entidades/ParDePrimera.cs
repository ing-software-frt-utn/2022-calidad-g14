using ControlDeCalidad.Dominio.EntidadesBase;

namespace ControlDeCalidad.Dominio.Entidades
{
    public class ParDePrimera : EntidadPersistible
    {
        public ParDePrimera() { }

        public ParDePrimera(short valor, short horaPlanilla, DateTime horaReal)
        {
            Valor = valor;

            HoraPlanilla = horaPlanilla;
            HoraReal = horaReal;
        }

        public short HoraPlanilla { get; set; }
        public DateTime HoraReal { get; set; }

        public short Valor { get; set; }
    }
}
