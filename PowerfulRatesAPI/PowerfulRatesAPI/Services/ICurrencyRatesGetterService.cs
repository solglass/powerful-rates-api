using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerfulRatesAPI
{
    public interface ICurrencyRatesGetterService
    {
        Task<Dictionary<string, decimal>> GetCurrencyRatesAsync();
    }
}