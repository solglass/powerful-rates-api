using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerfulRatesAPI
{
    public interface ICurrencyRatesService
    {
        Task<Dictionary<string, decimal>> GetCurrencyRates();
    }
}