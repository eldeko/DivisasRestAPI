using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivisasRestApi.Models
{
    public class Divisa
    {
        public string Casa { get; set; }
        public string Compra { get; set; }
        public string Venta { get; set; }
        public string Variacion { get; set; }
    }
}
