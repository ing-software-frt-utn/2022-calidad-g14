using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Delegados;
using ControlDeCalidad.Dominio.Entidades;

namespace ControlDeCalidad.Aplicacion.Servicios
{
    public class AlertasService : IAlertasService
    {
        private IRepository<OrdenDeProduccion> _ordenesRepo;
        public AlertasService(IRepository<OrdenDeProduccion> ordenesRepo)
        {
            _ordenesRepo = ordenesRepo;
        }

        public void SuscribirASemaforo(string numOp, LimiteInferiorDelegate handlerInferior, LimiteSuperiorDelegate handlerSuperior)
        {
            OrdenDeProduccion op = _ordenesRepo.BuscarPor(o => o.Numero == numOp).Single();
            op.LimiteInferior += handlerInferior;
            op.LimiteSuperior += handlerSuperior;
        }
    }
}
