using System.Collections.Generic;

namespace PowerfulRatesAPI
{
    public interface ICurrencyRatesService
    {
        Dictionary<string, decimal> GetCurrencyRates();
    }
}