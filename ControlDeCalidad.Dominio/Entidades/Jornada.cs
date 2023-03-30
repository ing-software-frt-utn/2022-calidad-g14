using ControlDeCalidad.Dominio.EntidadesBase;
using ControlDeCalidad.Dominio.Enumeraciones;

namespace ControlDeCalidad.Dominio.Entidades
{
    public class Jornada : EntidadPersistible
    {
        public Jornada() { }

        public Jornada(Empleado supCalidad, Turno turno)
        {
            SupervisorDeCalidad = supCalidad;
            Turno = turno;
            FechaInicio = DateTime.Now;
            FechaFin = null;

            ParesDePrimera = new List<ParDePrimera>();
            RegistrosDeDefecto = new List<RegistroDeDefecto>();
        }

        public Turno Turno { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public Empleado SupervisorDeCalidad { get; set; }

        public int TotalHermanado { get; set; }
        public int TotalSegunda { get; set; }

        public List<ParDePrimera> ParesDePrimera { get; set; }
        public List<RegistroDeDefecto> RegistrosDeDefecto { get; set; }

        // Para poder actualizar el front con los valores de cada hora de la planilla
        // se necesita una forma de buscar las cantidades segun la hora.
        // Esto se puede hacer en esta clase o en capas superiores.
        public int GetTotalDePrimera(short hora)
        {
            return ParesDePrimera.Where(pdp => pdp.Valor == 1 && pdp.HoraPlanilla == hora).Count();
        }

        public int GetTotalDeDefectos(short hora, Defecto defecto) 
        {
            return RegistrosDeDefecto
                .Where(r => r.Defecto == defecto && r.HoraPlanilla == hora)
                .Sum(r => r.Valor);
        }

        public int GetTotalDeDefectos(TipoDeDefecto tipo)
        {
            return RegistrosDeDefecto
               .Where(r => r.Defecto.Tipo == tipo && r.Valor == 1)
               .Count();
        }

        public void RegistrarParDePrimera(short valor, short horaPlanilla, DateTime horaReal)
        {
            if (Turno.DentroDeTurno(horaReal))
            {
                ParDePrimera parDePrimera = new ParDePrimera(valor, horaPlanilla, horaReal);
                ParesDePrimera.Add(parDePrimera);
            }
        }

        public void RegistrarDefecto(
            short valor, Pie pie, Defecto infoDefecto, short horaPlanilla, DateTime horaReal)
        {
            if (Turno.DentroDeTurno(horaReal))
            {
                RegistroDeDefecto registro =
                    new RegistroDeDefecto(pie, infoDefecto, valor, horaPlanilla, horaReal);
                RegistrosDeDefecto.Add(registro);
            }
        }
    }
}
