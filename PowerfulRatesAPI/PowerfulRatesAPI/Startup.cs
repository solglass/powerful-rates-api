using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerfulRatesAPI.Config;
using PowerfulRatesAPI.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PowerfulRatesAPI
{
    public class Startup
    {
        private static IConfiguration _configuration;
        static Startup()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }
        public static IServiceCollection ConfigureServices(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            services.RegistrateServicesConfig();
            services.Configure<AppSettings>(_configuration);

            return services;
        }
    }
}
