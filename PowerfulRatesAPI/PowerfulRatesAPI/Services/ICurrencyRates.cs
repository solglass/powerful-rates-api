using System.Collections.Generic;

namespace PowerfulRatesAPI
{
    public interface ICurrencyRates
    {
        Dictionary<string, decimal> GetCurrencyRates();
    }
}