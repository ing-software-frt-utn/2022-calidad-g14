using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Aplicacion.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ControlDeCalidad.ServiciosDistribuidos.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LineasController : ControllerBase
    {
        private IConsultaService<LineaDeTrabajo> _consultaService;

        public LineasController(IConsultaService<LineaDeTrabajo> consultaService)
        {
            _consultaService = consultaService;
        }

        // GET: api/Lineas
        [HttpGet]
        public IEnumerable<LineaDeTrabajoDTO> ObtenerLineas()
        {
            return _consultaService.ObtenerTodas().Select(l => new LineaDeTrabajoDTO(l));
        }

        //GET: api/Lineas/1
        [HttpGet("{numero}")]
        public IActionResult ObtenerLinea(int numero)
        {
            LineaDeTrabajo? linea = _consultaService.BuscarPor(l => l.Numero == numero).SingleOrDefault();

            return linea == null ? NotFound() : Ok(new LineaDeTrabajoDTO(linea));
        }
    }
}
