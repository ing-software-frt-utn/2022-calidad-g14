using ControlDeCalidad.Aplicacion.Contratos;
using ControlDeCalidad.Aplicacion.DTOs;
using ControlDeCalidad.Dominio.Contratos;
using ControlDeCalidad.Dominio.Entidades;

namespace ControlDeCalidad.Aplicacion.Servicios
{
    public class GestionColorService : IGestionColorService
    {
        private IRepository<Color> _colorRepo;

        public GestionColorService(IRepository<Color> colorRepo)
        {
            _colorRepo = colorRepo;
        }

        public void CrearColor(ColorDTO color)
        {
            Color nuevoColor = ColorDTO.DTOAColor(color);
            Color? colorRepositorio =
                _colorRepo.BuscarPor(c => c.Codigo == nuevoColor.Codigo).SingleOrDefault();

            if(colorRepositorio == null)
            { 
                _colorRepo.Agregar(nuevoColor);
                _colorRepo.UnidadDeTrabajo.Confirmar();
            }
        }

        public void ActualizarColor(ColorDTO color)
        {
            Color nuevoColor = ColorDTO.DTOAColor(color);
            Color? colorRepositorio =
                _colorRepo.BuscarPor(c => c.Codigo == nuevoColor.Codigo).SingleOrDefault();

            if (colorRepositorio != null)
            {
                _colorRepo.Modificar(nuevoColor);
                _colorRepo.UnidadDeTrabajo.Confirmar();
            }
        }

        public void EliminarColor(string codigo)
        {
            Color? color = _colorRepo.BuscarPor(c => c.Codigo == codigo).SingleOrDefault();

            if (color != null)
            {
                _colorRepo.Eliminar(color);
                _colorRepo.UnidadDeTrabajo.Confirmar();
            }
        }
    }
}
