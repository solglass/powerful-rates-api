using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace PowerfulRatesAPI
{

    public class CurrencyRates : ICurrencyRates
    {

        private string _url;
        private RestClient _client;
        private RestRequest _request;

        public CurrencyRates()
        {

        }
        public CurrencyRates(IConfiguration config)
        {
            _url = config.GetSection("CurrencyRatesSource").Value;
            _client = new RestClient(_url);
            _request = new RestRequest(Method.GET);
        }

        public Dictionary<string, decimal> GetCurrencyRates()
        {
            var response = _client.Execute<string>(_request);
            var json = JObject.Parse(response.Data);
            var result = json["price"].Select(s => new
            {
                CurrencyName = (s as JProperty).Name,
                CurrencyValue = (s as JProperty).Value
            })
            .ToDictionary(k => k.CurrencyName, v => Convert.ToDecimal(v.CurrencyValue));
            return result;
        }
    }
}
