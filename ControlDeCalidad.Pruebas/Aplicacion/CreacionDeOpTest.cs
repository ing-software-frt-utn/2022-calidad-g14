using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Aplicacion.Servicios;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Dominio.Enumeraciones;
using ControlDeCalidad.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using Autofac.Extras.Moq;

namespace ControlDeCalidad.Pruebas.Aplicacion
{
    public class CreacionDeOpTest
    {
        private List<OrdenDeProduccion> _ordenes = new List<OrdenDeProduccion>();

        [Fact]
        public void CreacionDeOpExitosa()
        {
            using(var mock = AutoMock.GetLoose())
            {
                ConfigurarMock(mock);
                Modelo modelo = ObtenerModelosMock()[0];
                Color color = ObtenerColoresMock()[0];
                LineaDeTrabajo lineaTrabajo = ObtenerLineasMock()[0];
                Empleado supervisorLinea = ObtenerEmpleadosMock()[0];
                int cantidadEsperada = _ordenes.Count + 1;
                
                OrdenDeProduccion nuevaOP =
                new OrdenDeProduccion("ABC123", color, modelo, supervisorLinea, lineaTrabajo);

                _ordenes.Add(nuevaOP);
                Assert.Equal(cantidadEsperada, _ordenes.Count);
                _ordenes.Clear();
            }
        }

        [Theory]
        [InlineData("ABC123", "M1", "C1", 2, 2)]
        [InlineData("ABC123", "M2", "C1", 3, 3)]
        public void CreacionDeOpConElMismoNumeroDeOtra(
            string numOp, string sku, string codColor, 
            int linea, int dni)
        {
            using(AutoMock mock = AutoMock.GetLoose())
            {
                //ConfigurarMock(mock);
                //ICrearOpService servicio = mock.Create<CrearOpService>();
                //_ordenes.Add(new OrdenDeProduccion { Numero = numOp}); 

                //Assert.Throws<NumeroDeOpOcupadoException>(() => servicio.CrearOp(numOp, sku, codColor, linea, dni));
                //_ordenes.Clear();
            }
        }

        [Theory]
        [InlineData("o.-rja")]
        [InlineData("3d")]
        [InlineData("84j1k205n393025nAFGASds8194ns")]
        [InlineData("R435!j$6")]
        [InlineData("")]
        public void CreacionDeOpConNumeroDeOpNoValido(string numOp)
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                //ConfigurarMock(mock);
                //ICrearOpService servicio = mock.Create<CrearOpService>();
               
                //Assert.Throws<FormatException>(() => servicio.CrearOp(numOp, "A1", "C1", 1, 1));
            }          
        }

        [Theory]
        [InlineData("ABC123", "M1", "C4", 2)]
        [InlineData("456XYZ", "M1", "C4", 2)]
        public void SupervisorIntentaCrearOpCuandoEstaVinculadoAOtra(string numOp, string sku, string color, int linea)
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                //ConfigurarMock(mock);
                //ICrearOpService servicio = mock.Create<CrearOpService>();
                //Empleado supOcupado = ObtenerEmpleadosMock()[0];
                //LineaDeTrabajo lineaOcupada = ObtenerLineasMock()[0];
                //_ordenes.Add(new OrdenDeProduccion { SupervisorDeLinea = supOcupado, Linea = lineaOcupada});

                //// Cambiar a excepcion descriptiva
                //Assert.Throws<Exception>(() => servicio.CrearOp(numOp, sku, color, linea, supOcupado.DNI));
                //_ordenes.Clear();
            }           
        }

        [Theory]
        [InlineData("ABC123", "M1", "C1", 2)]
        [InlineData("ASD156", "M1", "C1", 2)]
        public void CreacionDeOpEnUnaLineaOcupada(string numOp, string sku, string color, int supDni)
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                //ConfigurarMock(mock);
                //ICrearOpService servicio = mock.Create<CrearOpService>();
                //Empleado supervisor = ObtenerEmpleadosMock()[0];
                //LineaDeTrabajo lineaOcupada = ObtenerLineasMock()[0];
                //_ordenes.Add(new OrdenDeProduccion { SupervisorDeLinea = supervisor, Linea = lineaOcupada });

                //Assert.Throws<LineaOcupadaException>(() => servicio.CrearOp(numOp, sku, color, lineaOcupada.Numero, supDni));
                //_ordenes.Clear();
            }
        }

        [Theory]
        [InlineData(null, "A1", "C1", 1, 1)]
        [InlineData("ABC123", null, "C1", 1, 1)]
        [InlineData("ABC123", "A1", null, 1, 1)]
        [InlineData("ABC123", "A1", "C1", 0, 1)]
        [InlineData("ABC123", "A1", "C1", 1, 0)]
        public void CreacionDeOpConDatosFaltantes(string numOp, string sku, string codColor, int linea, int dni)
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                //ConfigurarMock(mock);
                //ICrearOpService servicio = mock.Create<CrearOpService>();

                //Assert.Throws<ArgumentException>(() => servicio.CrearOp(numOp, sku, codColor, linea, dni));
            }
        }

        // Para Moq. Podria moverse a otro archivo.


        private void ConfigurarMock(AutoMock mock)
        {
            mock.Mock<IRepository<Modelo>>()
                .Setup(x => x.ObtenerTodos())
                .Returns(ObtenerModelosMock());

            mock.Mock<IRepository<Color>>()
                .Setup(x => x.ObtenerTodos())
                .Returns(ObtenerColoresMock());

            mock.Mock<IRepository<LineaDeTrabajo>>()
                .Setup(x => x.ObtenerTodos())
                .Returns(ObtenerLineasMock());

            mock.Mock<IRepository<Empleado>>()
                .Setup(x => x.ObtenerTodos())
                .Returns(ObtenerEmpleadosMock());

            mock.Mock<IRepository<OrdenDeProduccion>>()
                .Setup(x => x.ObtenerTodos())
                .Returns(_ordenes);

            mock.Mock<IRepository<OrdenDeProduccion>>()
                .Setup(x => x.Agregar(It.IsAny<OrdenDeProduccion>()))
                .Callback(AgregarOpMock);
        }

        private void AgregarOpMock(OrdenDeProduccion op)
        {
            _ordenes.Add(op);
        }

        private List<Modelo> ObtenerModelosMock()
        {
            var output = new List<Modelo>
            {
                new Modelo("M1", "Modelo1", 1, 2, 1, 2),
                new Modelo("M2", "Modelo2", 1, 2, 1, 2)
            };
            return output;
        }

        private List<Color> ObtenerColoresMock()
        {
            var output = new List<Color>
            {
                new Color("C1", "Color1"),
                new Color("C2", "Color2")
            };
            return output;
        }

        private List<LineaDeTrabajo> ObtenerLineasMock()
        {
            var output = new List<LineaDeTrabajo>
            {
                new LineaDeTrabajo(1),
                new LineaDeTrabajo(2),
                new LineaDeTrabajo(3)
            };
            return output;
        }

        private List<Empleado> ObtenerEmpleadosMock()
        {
            var output = new List<Empleado>
            {
                new Empleado(1, "Leonel", "Lorca", "correo1", PuestoDeTrabajo.SupervisorDeLinea),
                new Empleado(2, "Matias", "Lucero", "correo2", PuestoDeTrabajo.SupervisorDeLinea),
                new Empleado(3, "Moises", "Salem", "correo3", PuestoDeTrabajo.SupervisorDeLinea)
            };
            return output;
        }
    }
}
