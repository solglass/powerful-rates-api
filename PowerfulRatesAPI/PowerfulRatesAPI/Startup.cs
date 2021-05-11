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

namespace PowerfulRatesAPI
{
    public class Startup
    {
        private static IConfiguration _configuration;
        private static IServiceProvider _serviceProvider;
        static Startup()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                //.AddCommandLine(args)
                .Build();
            _serviceProvider = new ServiceCollection()
                .ConfigureServices(_configuration)
                .BuildServiceProvider();
        }

        public static async void ProvideServices()
        {
            _serviceProvider.GetService<IRabbitMqMassTransitBusService>().SetupTimer();
           await _serviceProvider.GetService<IRabbitMqMassTransitBusService>().StartBusAsync();
            _serviceProvider.GetService<IRabbitMqMassTransitBusService>().SendFirstMessage();
        }
        //public static void ConfigureServices(this IServiceCollection services)
        //{
        //    services.RegistrateServicesConfig();
        //    services.Configure<AppSettings>(_configuration);
        //}

        //private static IConfiguration SetupConfiguration(string[] args)
        //{
        //    return new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .AddEnvironmentVariables()
        //        .AddCommandLine(args)
        //        .Build();
        //}
    }
}
