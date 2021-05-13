using System.Threading.Tasks;

namespace PowerfulRatesAPI.Services
{
    public interface IRabbitMqMassTransitBusService
    {
        Task StartBusAsync();
        Task SendFirstMessage();
    }
}