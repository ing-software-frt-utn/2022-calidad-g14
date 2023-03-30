using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Aplicacion.DTOs;
using Microsoft.AspNetCore.Mvc;
using ControlDeCalidad.Aplicacion.Contratos;

namespace ControlDeCalidad.ServiciosDistribuidos.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        private readonly IGestionModeloService _gestionService;
        private readonly IConsultaService<Modelo> _consultaService;

        public ModelosController(
            IGestionModeloService gestionService,
            IConsultaService<Modelo> consultaService)
        {
            _gestionService = gestionService;
            _consultaService = consultaService;
        }

        // GET: api/Modelos
        [HttpGet]
        public IEnumerable<ModeloDTO> ObtenerModelos()
        {
            return _consultaService.ObtenerTodas().Select(m => new ModeloDTO(m));
        }

        // GET api/Modelos/1
        [HttpGet("{sku}")]
        public ModeloDTO ObtenerModeloSKU(string sku)
        {
            Modelo? modelo = _consultaService.BuscarPor(m => m.SKU == sku).SingleOrDefault();
            return new ModeloDTO(modelo);
        }
   
        [HttpPost]
        public IActionResult RegistrarModelo(
            string sku, string denominacion,
            int limiteInferiorObservado, int limiteSuperiorObservado,
            int limiteInferiorReproceso, int limiteSuperiorReproceso
        )
        {
            try
            {
                _gestionService.CrearModelo(
                    sku, denominacion,
                    limiteInferiorObservado, limiteSuperiorObservado,
                    limiteInferiorReproceso, limiteSuperiorReproceso);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        // No funciona con DTOs
        // PUT api/Modelos/1
        // Modifica un modelo
        [HttpPut("{sku}")]
        public IActionResult ActualizarModelo(
            string sku, string denominacion,
            int limiteInferiorObservado, int limiteSuperiorObservado,
            int limiteInferiorReproceso, int limiteSuperiorReproceso)
        {
            try
            {
                _gestionService.ActualizarModelo(
                    sku, denominacion,
                    limiteInferiorObservado, limiteSuperiorObservado,
                    limiteInferiorReproceso, limiteSuperiorReproceso);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        // DELETE api/Modelos/1
        [HttpDelete("{sku}")]
        public IActionResult EliminarModelo(string sku)
        {
            try
            {
                _gestionService.EliminarModelo(sku);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }
    }
}
