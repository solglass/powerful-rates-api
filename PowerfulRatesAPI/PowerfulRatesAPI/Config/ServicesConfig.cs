using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PowerfulRatesAPI.Services;

namespace PowerfulRatesAPI.Config
{
    public static class ServicesConfig
    {
        public static void RegistrateServicesConfig(this IServiceCollection services)
        {
            services.AddSingleton<ICurrencyRatesService, CurrencyRatesService>();
            services.AddSingleton<IRabbitMqMassTransitBusService, RabbitMqMassTransitBusService>();

        }
    }
}
