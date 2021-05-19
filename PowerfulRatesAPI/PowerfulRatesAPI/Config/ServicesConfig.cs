using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PowerfulRatesAPI.Services;

namespace PowerfulRatesAPI.Config
{
    public static class ServicesConfig
    {
        public static void RegisterServicesConfig(this IServiceCollection services)
        {
            services.AddSingleton<IPublisherService, PublisherService>();
            services.AddSingleton<ICurrencyRatesGetterService, CurrencyRatesGetterService>();
            services.AddSingleton<ICurrencyRatesSenderService, CurrencyRatesSenderService>();
        }
    }
}
