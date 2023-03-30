using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Aplicacion.Sesiones;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Dominio.Enumeraciones;
using ControlDeCalidad.Dominio.Excepciones;
using System.Linq;
using System.Text.RegularExpressions;

namespace ControlDeCalidad.Aplicacion.Servicios
{
    public class CrearOpService : ICrearOpService
    {
        private IRepository<Modelo> _repoModelos;
        private IRepository<Color> _repoColores;
        private IRepository<Empleado> _repoEmpleado;
        private IRepository<LineaDeTrabajo> _repoLineas;
        private IOrdenDeProduccionRepository _repoOrdenes;

        public CrearOpService(
            IRepository<Modelo> repoModelos,
            IRepository<Color> repoColores,
            IRepository<Empleado> repoEmpleado,
            IRepository<LineaDeTrabajo> repoLineas,
            IOrdenDeProduccionRepository repoOrdenes)
        {
            _repoModelos = repoModelos;
            _repoColores = repoColores;
            _repoEmpleado = repoEmpleado;
            _repoLineas = repoLineas;
            _repoOrdenes = repoOrdenes;
        }

        public void CrearOp(string numOP, string sku, string codColor, int numLinea, string token)
        {
            string unescapedToken = Uri.UnescapeDataString(token);
            Empleado supLinea = AdministradorDeSesiones.Instancia.ObtenerDatos(unescapedToken);
            if(supLinea.Puesto != PuestoDeTrabajo.SupervisorDeLinea)
            {
                throw new Exception("Solo un supervisor de linea puede crear una orden de produccion.");
            }

            List<OrdenDeProduccion> ordenes = _repoOrdenes.ObtenerTodos().ToList();

            // Se podria validar dentro de cada clase
            ValidarReglas(ordenes, numOP, sku, codColor, numLinea, supLinea.DNI);

            Modelo modeloOP = _repoModelos.BuscarPor(m => m.SKU == sku).Single();
            Color colorOP = _repoColores.BuscarPor(c => c.Codigo == codColor).Single();
            Empleado supervisorOP = _repoEmpleado.BuscarPor(s => s.DNI == supLinea.DNI).Single();
            LineaDeTrabajo lineaOP = _repoLineas.BuscarPor(l => l.Numero == numLinea).Single();

            OrdenDeProduccion nuevaOP = 
                new OrdenDeProduccion(numOP, colorOP, modeloOP, supervisorOP, lineaOP);           
            
            _repoOrdenes.Agregar(nuevaOP);
            _repoOrdenes.UnidadDeTrabajo.Confirmar();
        }

        // Validacion excesiva?
        private void ValidarReglas(List<OrdenDeProduccion> ordenes, string numOP, string sku, string codColor, int numLinea, int supDni)
        {
            // Aplicacion o dominio?
            bool datosNoNulos = DatosNoNulos(numOP, sku, codColor, numLinea, supDni);
            if (!datosNoNulos)
            {
                throw new ArgumentException();
            }

            bool formatoDeNumeroValido = FormatoDeNumeroValido(numOP);
            if (!formatoDeNumeroValido)
            {
                throw new FormatException("Número de OP no valido.");
            }

            bool numeroDisponible = NumeroDisponible(numOP);
            if (!numeroDisponible)
            {
                throw new NumeroDeOpOcupadoException();
            }

            bool supervisorLibre = SupervisorLibre(supDni);
            if (!supervisorLibre)
            {
                throw new Exception("El supervisor no se encuentra libre.");
            }

            bool lineaLibre = LineaLibre(numLinea);
            if (!lineaLibre)
            {
                throw new LineaOcupadaException();
            }
        }

        private bool LineaLibre(int numLinea)
        {
            OrdenDeProduccion? op = _repoOrdenes
                .BuscarPor(o => o.Linea.Numero == numLinea && o.Estado != EstadoOP.Finalizada)
                .SingleOrDefault();

            return op == null;
        }

        private bool SupervisorLibre(int supDNI)
        {
            OrdenDeProduccion? op = _repoOrdenes
                .BuscarPor(o => o.SupervisorDeLinea.DNI == supDNI && o.Estado != EstadoOP.Finalizada)
                .SingleOrDefault();

            return op == null;
        }

        private bool NumeroDisponible(string numOP)
        {
            OrdenDeProduccion? op = _repoOrdenes.BuscarPor(op => op.Numero == numOP).SingleOrDefault();
            return op == null;
        }

        private bool FormatoDeNumeroValido(string numeroOP)
        {
            string patron = @"^[0-9a-zA-Z]{3,20}$";
            return Regex.IsMatch(numeroOP, patron);
        }

        private bool DatosNoNulos(string numOP, string sku, string codColor, int numLinea, int supDni)
        {
            return numOP != null && sku != null && codColor != null && numLinea > 0 && supDni > 0;
        }
    }
}
