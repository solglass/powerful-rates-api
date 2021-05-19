using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using PowerfulRatesAPI.Settings;
using Microsoft.Extensions.Options;

namespace PowerfulRatesAPI
{

    public class CurrencyRatesGetterService : ICurrencyRatesGetterService
    {

        private string _url;
        private RestClient _client;
        private RestRequest _request;
        private const string _baseCurrencyPair = "USDUSD";
        private const decimal _baseCurrencyPairValue = 1;

        public CurrencyRatesGetterService(IOptions<AppSettings> options)
        {
            _url = options.Value.RATES_API_CURRENCY_RATES_SOURCE;
            _client = new RestClient(_url);
            _request = new RestRequest(Method.GET);
        }

        public async Task<Dictionary<string, decimal>> GetCurrencyRatesAsync()
        {
            try
            {
                var response = await _client.ExecuteAsync<string>(_request);
                return CreateCurrencyRatesDictionary(response.Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Dictionary<string, decimal> CreateCurrencyRatesDictionary(string currencyPairs)
        {
            var json = JObject.Parse(currencyPairs);
            var result = json["price"].Select(s => new
            {
                CurrencyName = (s as JProperty).Name,
                CurrencyValue = (s as JProperty).Value
            })
            .ToDictionary(k => k.CurrencyName, v => Convert.ToDecimal(v.CurrencyValue));
            result.Add(_baseCurrencyPair, _baseCurrencyPairValue);
            return result;
        }
    }
}
