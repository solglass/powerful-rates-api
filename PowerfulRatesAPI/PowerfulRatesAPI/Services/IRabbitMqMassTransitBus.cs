using System.Threading.Tasks;

namespace PowerfulRatesAPI.Services
{
    public interface IRabbitMqMassTransitBus
    {
        Task StartBusAsync();
    }
}