using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PowerfulRatesAPI.Config
{
   public static class ServicesConfig
    {
        public static void RegistrateServicesConfig(this IServiceCollection services)
        {
                services.AddLogging(cfg => cfg.AddConsole());
            }

        }
    }
