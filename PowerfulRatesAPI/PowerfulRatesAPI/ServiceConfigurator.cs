using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerfulRatesAPI.Config;
using PowerfulRatesAPI.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace PowerfulRatesAPI
{
    public static class ServiceConfigurator
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,  IConfiguration configuration)
        {
            services.RegistrateServicesConfig();
            services.Configure<AppSettings>(configuration);
            return services;
        }
    }
}
