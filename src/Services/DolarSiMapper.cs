using DivisasRestApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DivisasRestApi.Services
{
    public static class DolarSiMapper
    {
        public static DivisaData DolarSiToDivisaData(List<DolarSiResponse> dolarSiResponse)
        {
            var divisaData = new DivisaData();
            divisaData.Fuente = "DolarSi";
            divisaData.UltimaActualizacion = GetArgTime();

            foreach(var response in dolarSiResponse)
            {
                divisaData.Divisas.Add(MapCasaToDivisa(response));
            }                
           
            return divisaData;
        }

        private static DateTime GetArgTime()
        {
            var argTime = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            var time = TimeZoneInfo.ConvertTime(DateTime.Now, argTime);
            return time;
        }

        private static Divisa MapCasaToDivisa(DolarSiResponse response)
        {
            var divisa = new Divisa();

               divisa.Compra = response.Casa.Compra;
               divisa.Venta = response.Casa.Venta;
               divisa.Casa = response.Casa.Nombre;
               divisa.Variacion = string.IsNullOrEmpty(response.Casa.Variacion) ? "-" : CheckIfNotNegative(response.Casa.Variacion);
            
            return divisa;
        }

        private static string CheckIfNotNegative(string variacion)
        {
          if (variacion != "0" && !variacion.Contains("-"))
          {
              variacion =  variacion.Insert(0, "+");
          }
            return variacion;
        }
    }
}
