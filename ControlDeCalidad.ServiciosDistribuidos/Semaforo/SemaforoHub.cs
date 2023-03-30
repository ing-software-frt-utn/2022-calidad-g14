using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Aplicacion.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace ControlDeCalidad.ServiciosDistribuidos.Semaforo
{
    public class SemaforoHub : Hub<ISemaforoHub>
    {
        public async Task UnirseALinea(string linea, string numOp)
        {
            //_alertasService.SuscribirASemaforo(numOp, OnAlertaLimiteInferior, OnAlertaLimiteSuperior);
            await Groups.AddToGroupAsync(Context.ConnectionId, linea);
        }

        public async Task AbandonarLinea(string linea)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, linea);
        }

        public async Task AlertaLimiteInferior(string numOp, string linea, string tipo)
        {
            await Clients.Group(linea).AlertaLimiteInferior(numOp, linea, tipo);
        }

        public async Task AlertaLimiteSuperior(string numOp, string linea, string tipo)
        {
            await Clients.Group(linea).AlertaLimiteSuperior(numOp, linea, tipo);
        }

        public async Task ActualizarSemaforo(SemaforoDTO semaforoDTO) 
        {
            string linea = semaforoDTO.Linea.ToString();
            await Clients.Group(linea).ActualizarSemaforo(semaforoDTO);
        }

        // Pruebas:
        public async Task NuevoMensaje(string mensaje)
        {
            await Clients.Others.NuevoMensaje(mensaje);
        }
    }
}
