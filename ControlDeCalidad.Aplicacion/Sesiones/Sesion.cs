using ControlDeCalidad.Dominio.Entidades;
using System.Timers;

namespace ControlDeCalidad.Aplicacion.Sesiones
{
    internal class Sesion
    {
        private ElapsedEventHandler _administradorHandler;
        public Sesion(string token, Empleado empleado, int duracion, ElapsedEventHandler handler)
        {
            Token = token;
            Empleado = empleado;
            ConfigurarTimer(duracion, handler);
        }

        private System.Timers.Timer _timer;
        public readonly string Token;
        public Empleado Empleado { get; private set; }

        private void ConfigurarTimer(int duracion, ElapsedEventHandler handler)
        {
            _administradorHandler = handler;
            // Duracion en horas
            _timer = new System.Timers.Timer(duracion);
            _timer.Elapsed += SesionFinalizada;
            _timer.AutoReset = false;
            _timer.Start();
        }

        private void SesionFinalizada(object sender, ElapsedEventArgs args)
        {
            _administradorHandler?.Invoke(this, args);
        }
    }
}
