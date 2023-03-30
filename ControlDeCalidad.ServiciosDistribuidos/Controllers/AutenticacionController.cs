using Microsoft.AspNetCore.Mvc;
using ControlDeCalidad.ServiciosDistribuidos.Utiles;
using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Aplicacion.DTOs;
using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Aplicacion.Sesiones;

namespace ControlDeCalidad.ServiciosDistribuidos.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private IConsultaService<Empleado> _consultaService;

        public AutenticacionController(IConsultaService<Empleado> consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpGet]
        public IActionResult IniciarSesion(string correo, string password) //, string puesto, int numLinea = 0)
        {
            try
            {
                return AutenticarUsuario(correo, password);//, puesto, numLinea);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }
        
        private IActionResult AutenticarUsuario(string correo, string password) //, string puesto, int numLinea = 0)
        {            
            Empleado? empleado = _consultaService.BuscarPor(e => e.CorreoElectronico == correo).SingleOrDefault();

            if (empleado == null)
                return Unauthorized("Correo electronico incorrecto.");

            // Esto deberia ser de una capa transversal o estar en la capa de aplicacion
            string hashAlmacenado = EncriptacionUtil.ObtenerHash(empleado.DNI.ToString());
            string hashEntrante = EncriptacionUtil.ObtenerHash(password);
            bool hashesCoinciden = string.Equals(hashAlmacenado, hashEntrante);

            if (!hashesCoinciden)
                return Unauthorized("Contraseña Incorrecta.");

            // Verificar si puesto corresponde al puesto del objeto empleado?
            // Esto ayuda a que un supervisor no pueda ingresar a la terminal de 
            // un administrativo, por ejemplo

            #region Bloqueo de linea (no usado)
            //if(numLinea != 0)
            //{
            //    // Verificar si el supervisor tiene una op alguna linea
            //    // Si tiene una y el numero de linea es el mismo a numLinea, pasa
            //    // Si tiene una y el numero de linea no es el mismo a numLinea, no pasa
            //    // Se podria extraer en otro metodo
            //    var ordenesActivas =
            //        from op in _repoOrdenes.ObtenerTodos()
            //        where (op.Estado == EstadoOP.Iniciada || op.Estado == EstadoOP.Pausada) 
            //        select op;

            //    // Tratar de acceder a un solo subnivel
            //    OrdenDeProduccion? ordenVinculada = 
            //        ordenesActivas.FirstOrDefault(op => op.SupervisorDeLinea.DNI == empleado.DNI);

            //    if (ordenVinculada != null && ordenVinculada.Linea.Numero != numLinea)
            //        return Unauthorized(
            //            $"No puede iniciar sesión en esta linea hasta finalizar con la orden {ordenVinculada.Numero}"
            //            );
            //}
            #endregion Bloqueo de linea (no usado)

            string token = EncriptacionUtil.ObtenerHash(empleado.DNI.ToString())
                .Replace('+', '*'); // Solucion temporal
            SesionDTO sesion = AdministradorDeSesiones.Instancia.IniciarSesion(token, empleado);          
            return Ok(sesion);
        }

        [HttpPut]
        public IActionResult CerrarSesion(string token)
        {
            try
            {
                string unescapedToken = Uri.UnescapeDataString(token);
                AdministradorDeSesiones.Instancia.CerrarSesion(unescapedToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
