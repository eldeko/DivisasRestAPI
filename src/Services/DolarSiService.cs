using DivisasRestApi.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivisasRestApi.Services
{
    public class DolarSiService : IDolarSiService
    {
        public DivisaData GetDolarsiDivisada()
        {
            var client = new RestClient("https://www.dolarsi.com/api/api.php?type=valoresprincipales");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<List<DolarSiResponse>>(response.Content);

            var mappedresult = DolarSiMapper.DolarSiToDivisaData(result);

            return mappedresult;
        }

    }
}
