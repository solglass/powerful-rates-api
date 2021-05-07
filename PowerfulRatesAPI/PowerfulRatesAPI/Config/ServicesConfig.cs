using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PowerfulRatesAPI.Services;

namespace PowerfulRatesAPI.Config
{
    public static class ServicesConfig
    {
        public static void RegistrateServicesConfig(this IServiceCollection services, IConfiguration config)
        {
            services.AddLogging(cfg => cfg.AddConsole());
            services.AddSingleton<ICurrencyRates, CurrencyRates>();
            services.AddSingleton<IRabbitMqMassTransitBus, RabbitMqMassTransitBus>();

        }

    }
}
