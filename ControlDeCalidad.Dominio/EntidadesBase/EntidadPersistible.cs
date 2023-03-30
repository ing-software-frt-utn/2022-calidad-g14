using System.Text.Json.Serialization;

namespace ControlDeCalidad.Dominio.EntidadesBase
{
    public class EntidadPersistible
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}