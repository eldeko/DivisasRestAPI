namespace DivisasRestApi.Models
{
    public partial class DolarSiResponse
    {
        public Casa Casa { get; set; }
    }

    public partial class Casa
    {
        public string Nombre { get; set; }
        public string Compra { get; set; }
        public string Venta { get; set; }
        public long? Agencia { get; set; }
        public Observaciones Observaciones { get; set; }
        public Geolocalizacion Geolocalizacion { get; set; }
        public Direccion? Telefono { get; set; }
        public Direccion? Direccion { get; set; }
        public long? Decimales { get; set; }
        public string Variacion { get; set; }
    }

    public partial class Observaciones
    {
    }

    public partial class Geolocalizacion
    {
        public Direccion? Latitud { get; set; }
        public Direccion? Longitud { get; set; }
    }

    public partial struct Direccion
    {
        public Observaciones Observaciones;
        public string String;

        public static implicit operator Direccion(Observaciones Observaciones) => new Direccion { Observaciones = Observaciones };
        public static implicit operator Direccion(string String) => new Direccion { String = String };
    }

}
