using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivisasRestApi.Models
{
    public class DivisaData
    {
        public string Fuente { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public List<Divisa> Divisas { get; set; }
        
        public DivisaData()
        {
            Divisas = new List<Divisa>();
        }
    }
}
