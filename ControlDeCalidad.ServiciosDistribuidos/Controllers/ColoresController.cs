using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Aplicacion.DTOs;
using Microsoft.AspNetCore.Mvc;
using ControlDeCalidad.Aplicacion.Contratos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlDeCalidad.ServiciosDistribuidos.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ColoresController : ControllerBase
    {
        private IConsultaService<Color> _consultaService;
        private IGestionColorService _gestionService;

        public ColoresController(
            IConsultaService<Color> consultaService,
            IGestionColorService gestionService)
        {
            _consultaService = consultaService;
            _gestionService = gestionService;
        }

        // GET: api/Colores
        [HttpGet]
        public IEnumerable<ColorDTO> ObtenerColores()
        {
            return _consultaService.ObtenerTodas().Select(c => new ColorDTO(c));
        }

        // GET: api/Colores/1
        [HttpGet("{codigo}")]
        public IActionResult ObtenerColorCodigo(string codigo)
        {
            Color? color = _consultaService.BuscarPor(c => c.Codigo == codigo).SingleOrDefault();
            return color == null ? NotFound() : Ok(new ColorDTO(color));
        }

        // POST: api/Colores
        [HttpPost]
        public IActionResult RegistrarColor(ColorDTO color)
        {
            try
            {
                _gestionService.CrearColor(color);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        // PUT api/Colores/1
        [HttpPut("{codigo}")]
        public IActionResult ActualizarColor(string codigo, ColorDTO color)
        {
            try
            {
                _gestionService.ActualizarColor(color);
                return Ok();
            }
            catch(Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        //DELETE api/Colores/1
        [HttpDelete("{codigo}")]
        public IActionResult EliminarColor(string codigo)
        {
            try
            {
                _gestionService.EliminarColor(codigo);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }
    }
}