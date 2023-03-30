using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Aplicacion.DTOs;
using ControlDeCalidad.Dominio.Enumeraciones;
using ControlDeCalidad.ServiciosDistribuidos.Semaforo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ControlDeCalidad.ServiciosDistribuidos.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InspeccionController : ControllerBase
    {
        private IInspeccionService _inspeccionService;
        private readonly IHubContext<SemaforoHub, ISemaforoHub> _semaforoHub;

        public InspeccionController(
            IInspeccionService inspeccionService,
            IHubContext<SemaforoHub, ISemaforoHub> semaforoHub)
        {
            _inspeccionService = inspeccionService;
            _semaforoHub = semaforoHub;
        }

        [HttpPost]
        public async Task<IActionResult> ParDePrimera(string numOp, short valor, short hora, string token)
        {
            try
            {
                _inspeccionService.RegistrarParDePrimera(numOp, token, valor, hora);
                _inspeccionService.Dispose();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Defecto(string numOp, string codigoDefecto, int pie, short valor, short hora, string token)
        {
            try
            {
                Pie pieEnum = pie == 0 ? Pie.Izquierdo : Pie.Derecho;                
                SemaforoDTO dto = _inspeccionService.RegistrarDefecto(numOp, token, codigoDefecto, pieEnum, valor, hora);
                string linea = dto.Linea.ToString();
                _semaforoHub.Clients.Group(linea).ActualizarSemaforo(dto);
                _inspeccionService.Dispose();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Quizas no sea el mejor lugar para esta funcionalidad
        [HttpGet]
        public IActionResult ObtenerDatosEnLinea(int numeroLinea)
        {
            try
            {
                SemaforoDTO datos = _inspeccionService.ObtenerDatosEnLinea(numeroLinea);
                return Ok(datos);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }
    }
}
