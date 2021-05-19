using System.Threading.Tasks;

namespace PowerfulRatesAPI.Services
{
    public interface ICurrencyRatesSenderService
    {
        Task SendFirstMessage();
    }
}