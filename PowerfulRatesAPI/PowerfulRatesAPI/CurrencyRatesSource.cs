using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace PowerfulRatesAPI
{
    public static class CurrencyRatesSource
    {
        public static string url = Program.Configuration.GetSection("CurrencyRatesSource").Value;
        public static RestClient _client = new RestClient(url);
        public static RestRequest _request = new RestRequest(Method.GET);

        public static Dictionary<string, decimal> GetCurrencyRates()
        {
            var response = _client.Execute<string>(_request);
            var json = JObject.Parse(response.Data);
            var result = json["quotes"].Select(s => new
            {
                CurrencyName = (s as JProperty).Name,
                CurrencyValue = (s as JProperty).Value
            })
            .ToDictionary(k => k.CurrencyName, v => Convert.ToDecimal(v.CurrencyValue));
            return result;
        }
       
    }
}
