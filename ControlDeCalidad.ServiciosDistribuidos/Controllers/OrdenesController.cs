using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Dominio.Delegados;
using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Dominio.Enumeraciones;
using ControlDeCalidad.Aplicacion.DTOs;
using ControlDeCalidad.ServiciosDistribuidos.Semaforo;
using Microsoft.AspNetCore.Mvc;
using ControlDeCalidad.Aplicacion.Sesiones;
using Microsoft.AspNetCore.SignalR;
using ControlDeCalidad.Dominio.Contratos;

namespace ControlDeCalidad.ServiciosDistribuidos.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        // Se podria descomponer el control para disminuir las inyecciones
        private IConsultaService<OrdenDeProduccion> _consultaService;
        private ICrearOpService _crearOpService;
        private IGestionOpService _gestionOpService;
        private IAsociacionOpService _asociacionOpService;

        public OrdenesController(
            IConsultaService<OrdenDeProduccion> consultaService,
            ICrearOpService crearOpService,
            IGestionOpService gestionOpService,
            IAsociacionOpService asociacionOpService)
        {
            _consultaService = consultaService;
            _crearOpService = crearOpService;
            _gestionOpService = gestionOpService;
            _asociacionOpService = asociacionOpService;
        }

        // GET: api/Ordenes
        [HttpGet]
        public IEnumerable<OrdenDTO> ObtenerOPs()
        {
            return _consultaService.ObtenerTodas().Select(op => new OrdenDTO(op));
        }

        // GET api/Ordenes/5
        [HttpGet("{numero}")]
        public IActionResult ObtenerOPId(string numero)
        {
            try
            {
                OrdenDeProduccion orden = _consultaService.BuscarPor(op => op.Numero == numero).Single();
                return Ok(orden);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{token}")]
        public IActionResult MiOp(string token)
        {
            try
            {
                OrdenDTO orden = _gestionOpService.MiOp(token);
                return Ok(orden);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // POST api/Ordenes
        [HttpPost]
        public IActionResult CrearOP(string numOP, string sku, string codColor, int numLinea, string token)
        {
            try
            {
                _crearOpService.CrearOp(numOP, sku, codColor, numLinea, token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Las modificaciones de estado deberian pedir el token (mas seguro)
        [HttpPut]
        public IActionResult ReanudarOp(string numOP, string token)
        {
            try
            {
                // Suscripcion al semaforo
                _gestionOpService.ReanudarOp(numOP, token);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult PausarOp(string numOP, string token)
        {
            try
            {
                // Desuscripcion al semaforo
                _gestionOpService.PausarOp(numOP, token);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult FinalizarOp(string numOP, string token)
        {
            try
            {
                // Desuscripcion al semaforo
                _gestionOpService.FinalizarOp(numOP, token);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> AsociarAOp(string numOP, string token)
        {
            try
            {
                string unescapedToken = Uri.UnescapeDataString(token);
                _asociacionOpService.AsociarAOp(unescapedToken, numOP);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Podria (o deberia) ser el token
        [HttpPut]
        public IActionResult DesasociarAOp(string numOp)
        {
            try
            {
                _asociacionOpService.DesasociarAOp(numOp);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}