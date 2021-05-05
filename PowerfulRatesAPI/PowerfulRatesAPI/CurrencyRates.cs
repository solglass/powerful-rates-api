using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace PowerfulRatesAPI
{
    public static class CurrencyRates
    {
        public static string url = Program.Configuration.GetSection("CurrencyRatesSourse").Value;
        public static RestClient _client = new RestClient(url);
        public static RestRequest _request = new RestRequest(Method.GET);

        public static string GetCurrencyRates()
        {
            var response = _client.Execute<string>(_request);
            return response.Data;
        }
       
    }
}
