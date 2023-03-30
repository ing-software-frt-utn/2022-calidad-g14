using ControlDeCalidad.Aplicacion.DTOs;
using ControlDeCalidad.Dominio.Entidades;
using System.Timers;

namespace ControlDeCalidad.Aplicacion.Sesiones
{
    public class AdministradorDeSesiones
    {
        private Dictionary<string, Sesion> _sesiones;
        private const int DURACION_HORAS = 5 * 3600000;

        private static volatile AdministradorDeSesiones _instancia;
        private static readonly object _lock = new object();
        private AdministradorDeSesiones()
        {
            _sesiones = new Dictionary<string, Sesion>();
        }

        public static AdministradorDeSesiones Instancia
        {
            get
            {
                lock (_lock)
                {
                    if (_instancia == null)
                        _instancia = new AdministradorDeSesiones();
                }
                return _instancia;
            }
        }

        public SesionDTO IniciarSesion(string token, Empleado empleado)
        {
            if(!_sesiones.ContainsKey(token))
            {
                Sesion sesion = new Sesion(token, empleado, DURACION_HORAS, OnSesionExpirada);
                _sesiones.Add(token, sesion);
            }
            return new SesionDTO(token, new EmpleadoDTO(empleado));
        }

        public void CerrarSesion(string token)
        {
            if (_sesiones.ContainsKey(token))
            {
                _sesiones.Remove(token);
            }
        }

        public Empleado ObtenerDatos(string token)
        {
            if (_sesiones.ContainsKey(token))
            {
                return _sesiones[token].Empleado;
            }
            else
            {
                // No solo pasa cuando expira, quizas no sea muy descriptivo
                throw new Exception("La sesion ha expirado.");
            }
        }

        public void OnSesionExpirada(object sender, ElapsedEventArgs args)
        {
            Sesion? sesionExpirada = sender as Sesion;
            if(sesionExpirada != null)
            {
                CerrarSesion(sesionExpirada.Token);
            }
        }
    }
}
