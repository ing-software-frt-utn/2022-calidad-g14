using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Dominio.Enumeraciones;
using System;
using Xunit;
using Moq;
using Autofac.Extras.Moq;
using ControlDeCalidad.Dominio.Contratos;
using System.Collections.Generic;
using System.Linq;

namespace ControlDeCalidad.UnitTests.Dominio
{
    public class JornadaTests
    {
        private Empleado _supCalidad;

        public JornadaTests()
        {
            _supCalidad = new Empleado(89514351, "Luis", "Perez", "Correo", PuestoDeTrabajo.SupervisorDeCalidad);
        }

        [Fact]
        public void SeRegistraUnPareDePrimeraDentroDelTurno()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                ConfigurarMock(mock);
                Turno turno = ObtenerTurnosMock()[0];
                turno.DentroDeTurno(DateTime.Now);
                Jornada jornada = new Jornada(_supCalidad, turno);

                jornada.RegistrarParDePrimera(1, 5, DateTime.Now);

                Assert.Single(jornada.ParesDePrimera);
            }
        }

        [Fact]
        public void SeRegistraUnParDePrimeraFueraDelTurno()
        {
            //using (AutoMock mock = AutoMock.GetLoose())
            //{
            //    var turno = new Mock<Turno>();
            //    turno.Setup(t => t.DentroDeTurno(It.IsAny<TimeOnly>())).Returns(false);
            //    Jornada jornada = new Jornada(_supCalidad, turno.Object);

            //    jornada.RegistrarParDePrimera(1, 5);

            //    Assert.Single(jornada.ParesDePrimera);
            //}
        }

        // Probar tipos de defecto por separado?
        [Theory]
        [InlineData(1, Pie.Derecho, "DR1", 10)]
        [InlineData(1, Pie.Izquierdo, "DO1", 11)]
        public void SeRegistraUnDefectoDentroDelTurno(short valor, Pie pie, string codDefecto, short horaPlanilla)
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                ConfigurarMock(mock);
                Turno turno = ObtenerTurnosMock()[0];
                turno.DentroDeTurno(DateTime.Now);
                Jornada jornada = new Jornada(_supCalidad, turno);
                Defecto defecto = ObtenerDefectosMock().Where(x => x.Codigo == codDefecto).First();

                jornada.RegistrarDefecto(valor, pie, defecto, horaPlanilla, DateTime.Now);

                Assert.Single(jornada.RegistrosDeDefecto);
            }

        }

        [Fact]
        public void SeRegistraUndefectoFueraDelTurno()
        {

        }

        [Fact] 
        public void SeCorrigeUnRegistroDeParDePrimeraDentroDelTurno()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                ConfigurarMock(mock);
                Turno turno = ObtenerTurnosMock()[0];
                turno.DentroDeTurno(DateTime.Now);
                Jornada jornada = new Jornada(_supCalidad, turno);

                jornada.RegistrarParDePrimera(-1, 5, DateTime.Now);

                Assert.Single(jornada.ParesDePrimera);
            }

        }

        [Theory]
        [InlineData(-1, Pie.Derecho, "DR1", 10)]
        [InlineData(-1, Pie.Izquierdo, "DO1", 11)]
        public void SeCorrigeUnRegistroDeDefectoDentroDelTurno(short valor, Pie pie, string codDefecto, short horaPlanilla)
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                ConfigurarMock(mock);
                Turno turno = ObtenerTurnosMock()[0];
                turno.DentroDeTurno(DateTime.Now);
                Jornada jornada = new Jornada(_supCalidad, turno);
                Defecto defecto = ObtenerDefectosMock().Where(x => x.Codigo == codDefecto).First();

                jornada.RegistrarDefecto(valor, pie, defecto, horaPlanilla, DateTime.Now);

                Assert.Single(jornada.RegistrosDeDefecto);
            }

        }

        private void ConfigurarMock(AutoMock mock)
        {
            mock.Mock<IRepository<Turno>>()
                .Setup(x => x.ObtenerTodos())
                .Returns(ObtenerTurnosMock());

            mock.Mock<IRepository<Defecto>>()
                .Setup(x => x.ObtenerTodos())
                .Returns(ObtenerDefectosMock());
        }

        private List<Turno> ObtenerTurnosMock()
        {
            var output = new List<Turno>
            {
                new Turno(8, 00, "Completo")
            };
            return output;
        }

        private List<Defecto> ObtenerDefectosMock()
        {
            var output = new List<Defecto>
            {
                new Defecto("DR1", "Suela Despegada", TipoDeDefecto.Reproceso),
                new Defecto("DO1", "Suela Despegada", TipoDeDefecto.Observado)
            };
            return output;
        }
    }
}