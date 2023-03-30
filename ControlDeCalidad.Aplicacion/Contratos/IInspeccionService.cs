using ControlDeCalidad.Aplicacion.DTOs;
using ControlDeCalidad.Dominio.Enumeraciones;

namespace ControlDeCalidad.Aplicacion.Contratos
{
    public interface IInspeccionService
    {
        void RegistrarParDePrimera(string numOp, string token, short valor, short hora);
        SemaforoDTO RegistrarDefecto(string numOp, string token, string codigoDefecto, Pie pie, short valor, short hora);
        SemaforoDTO ObtenerDatosEnLinea(int numeroLinea);
        void Dispose();
    }
}
