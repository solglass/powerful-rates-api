using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerfulRatesAPI.Config;
using PowerfulRatesAPI.Services;
using PowerfulRatesAPI.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PowerfulRatesAPI
{
    public class Startup
    {
        private IConfiguration _configuration;
        private IServiceProvider _serviceProvider;
        public Startup()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            _serviceProvider = new ServiceCollection()
                .ConfigureServices(_configuration)
                .BuildServiceProvider();
        }

        public async Task ProvideServices()
        {
           await _serviceProvider.GetService<IRabbitMqMassTransitBusService>().StartBusAsync();
           _serviceProvider.GetService<IRabbitMqMassTransitBusService>().SendFirstMessage();
        }
    }
}
