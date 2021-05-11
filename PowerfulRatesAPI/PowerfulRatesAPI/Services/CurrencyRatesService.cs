using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using PowerfulRatesAPI.Settings;
using Microsoft.Extensions.Options;

namespace PowerfulRatesAPI
{

    public class CurrencyRatesService : ICurrencyRatesService
    {

        private string _url;
        private RestClient _client;
        private RestRequest _request;

        public CurrencyRatesService()
        {

        }
        public CurrencyRatesService(IOptions<AppSettings> options)
        {
            _url = options.Value.CURRENCY_RATES_SOURCE;
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
