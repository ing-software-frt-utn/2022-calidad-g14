using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.Dominio.Enumeraciones;
using Microsoft.AspNetCore.SignalR;

namespace ControlDeCalidad.ServiciosDistribuidos.Semaforo
{
    // Mover a capa de aplicacion?
    public class SemaforoHandler : ISemaforoHandler
    {
        private IHubContext<SemaforoHub, ISemaforoHub> _semaforoHub;
        public SemaforoHandler(IHubContext<SemaforoHub, ISemaforoHub> semaforoHub)
        {
            _semaforoHub = semaforoHub;
        }

        public void OnAlertaLimiteInferior(OrdenDeProduccion op, TipoDeDefecto tipo)
        {
            string linea = op.Linea.Numero.ToString();
            string numOp = op.Numero.ToString();
            string tipoDeDefecto = tipo.ToString();
            _semaforoHub.Clients.Group(linea).AlertaLimiteInferior(numOp, linea, tipoDeDefecto);
        }

        public void OnAlertaLimiteSuperior(OrdenDeProduccion op, TipoDeDefecto tipo)
        {
            string linea = op.Linea.Numero.ToString();
            string numOp = op.Numero.ToString();
            string tipoDeDefecto = tipo.ToString();
            _semaforoHub.Clients.Group(linea).AlertaLimiteSuperior(numOp, linea, tipoDeDefecto);
        }
    }
}