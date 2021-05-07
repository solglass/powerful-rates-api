using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace PowerfulRatesAPI
{

    public class CurrencyRates : ICurrencyRates
    {

        private string _url;
        private RestClient _client;
        private RestRequest _request;

        public CurrencyRates(IConfiguration config)
        {
            _url = config.GetSection("CurrencyRatesSource").Value;
            _client = new RestClient(_url);
            _request = new RestRequest(Method.GET);
        }

        public string GetCurrencyRates()
        {

            var response = _client.Execute<string>(_request);
            return response.Data;
        }
    }
}
