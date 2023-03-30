using ControlDeCalidad.Aplicacion.DTOs;

namespace ControlDeCalidad.ServiciosDistribuidos.Semaforo
{
    public interface ISemaforoHub
    {
        
        Task UnirseALinea(string linea);
        Task AbandonarLinea(string linea);
        Task AlertaLimiteInferior(string numOp, string linea, string tipo);
        Task AlertaLimiteSuperior(string numOp, string linea, string tipo);
        Task ActualizarSemaforo(SemaforoDTO semaforoDTO);
        Task NuevoMensaje(string mensaje);
    }
}
