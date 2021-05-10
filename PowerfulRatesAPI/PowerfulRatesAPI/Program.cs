using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EventContracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerfulRatesAPI.Services;

namespace PowerfulRatesAPI
{
    class Program
    {
        public static async Task Main(string[] args)
        {

            var services = Startup.ConfigureServices(args);

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<IRabbitMqMassTransitBus>().SetupTimer();
            await serviceProvider.GetService<IRabbitMqMassTransitBus>().StartBusAsync();
            serviceProvider.GetService<IRabbitMqMassTransitBus>().SendFirstMessage();
            Console.ReadLine();
        }

    }



}
