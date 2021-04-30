using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace PowerfulRatesWebAPI
{
    public static class CurrencyRates
    {
        public const string url = "http://api.currencylayer.com/live?access_key=e4f2e8790e61ca39430fd98df8bd299b";
        public static RestClient _client = new RestClient(url);
        public static RestRequest _request = new RestRequest(Method.GET);

        public static string GetCurrencyRates()
        {
            var response = _client.Execute<string>(_request);
            return response.Data;
        }
       
    }
}
