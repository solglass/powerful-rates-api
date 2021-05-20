using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Options;
using PowerfulRatesAPI.Settings;

namespace PowerfulRatesAPI.Services
{
    public class PublisherService : IPublisherService
    {
        private IBusControl _busControl;
        public PublisherService(IOptions<AppSettings> options)
        {
            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg => cfg.Host(options.Value.RATES_API_RABBITMQ_HOST, hst =>
            {
                hst.Username(options.Value.RATES_API_RABBITMQ_LOGIN);
                hst.Password(options.Value.RATES_API_RABBITMQ_PASSWORD);
            }));
            _busControl.StartAsync();
        }

        public async Task PublishAsync(object message)
        {
            await _busControl.Publish(message);
        }
    }
}

