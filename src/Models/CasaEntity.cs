using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class CasaEntity
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("compra")]
        public string Compra { get; set; }

        [JsonProperty("venta")]
        public string Venta { get; set; }       
    }
}