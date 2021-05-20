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
using EventContracts;
using PowerfulRatesAPI.CustomExceptions;

namespace PowerfulRatesAPI.Services
{

    public class CurrencyRatesGetterService : ICurrencyRatesGetterService
    {

        private string _url;
        private RestClient _client;
        private RestRequest _request;
        private IRestResponse<string> _response;
        private const string _baseCurrencyPair = "USDUSD";
        private const decimal _baseCurrencyPairValue = 1;

        public CurrencyRatesGetterService(IOptions<AppSettings> options, IPublisherService publisherService)
        {
            _url = options.Value.RATES_API_CURRENCY_RATES_SOURCE;
            _client = new RestClient(_url);
            _request = new RestRequest(Method.GET);
        }

        public async Task<Dictionary<string, decimal>> GetCurrencyRatesAsync()
        {
            _response = await _client.ExecuteAsync<string>(_request);
            if (_response == null || _response.Data == null) throw new ServiceUnavailableException();
            return GetCurrencyRatesDictionary();
        }
        private Dictionary<string, decimal> GetCurrencyRatesDictionary()
        {
            var json = JObject.Parse(_response.Data);
            var result = json["price"].Select(s => new
            {
                CurrencyName = (s as JProperty).Name,
                CurrencyValue = (s as JProperty).Value
            })
            .ToDictionary(k => k.CurrencyName, v => Convert.ToDecimal(v.CurrencyValue));
            if (result.Count == 0) throw new ParsingException();
            result.Add(_baseCurrencyPair, _baseCurrencyPairValue);
            return result;
        }
    }
}
