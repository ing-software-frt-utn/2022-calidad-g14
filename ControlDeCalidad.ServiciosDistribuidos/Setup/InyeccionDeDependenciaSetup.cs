using ControlDeCalidad.AccesoADatos.Contextos;
using ControlDeCalidad.AccesoADatos.Contratos;
using ControlDeCalidad.AccesoADatos.Repositorios;
using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Aplicacion.Servicios;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;
using ControlDeCalidad.ServiciosDistribuidos.Semaforo;
using Microsoft.EntityFrameworkCore;

namespace ControlDeCalidad.ServiciosDistribuidos.Setup
{
    public static class InyeccionDeDependenciaSetup
    {
        public static IServiceCollection Configurar(this IServiceCollection servicios, string conectionString)
        {
            servicios.AddControllers();
           
            servicios.AddEndpointsApiExplorer();
            servicios.AddSwaggerGen();
            
            servicios.AddDbContext<IUnidadDeTrabajoEF, ControlContexto>(
                options => options.UseSqlServer(conectionString),
                ServiceLifetime.Scoped
                );

            servicios.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            servicios.AddScoped<IOrdenDeProduccionRepository, OrdenDeProduccionRepository>();

            servicios.AddScoped<ICrearOpService, CrearOpService>();
            servicios.AddScoped<IGestionOpService, GestionOpService>();
            servicios.AddScoped<IAsociacionOpService, AsociacionOpService>();
            servicios.AddScoped<IInspeccionService, InspeccionService>();
            servicios.AddScoped<IAlertasService, AlertasService>();
            servicios.AddScoped<IGestionModeloService, GestionModeloService>();
            servicios.AddScoped<IGestionColorService, GestionColorService>();
            servicios.AddScoped(typeof(IConsultaService<>), typeof(ConsultaService<>));

            // No se si es seguro en hilos, tal vez sea mejor usar patron:
            servicios.AddSingleton<ISemaforoHandler, SemaforoHandler>();

            servicios.AddSignalR();

            return servicios;
        }
    }
}
