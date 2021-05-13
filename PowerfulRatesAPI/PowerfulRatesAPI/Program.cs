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
            var startup = new Startup();
            await startup.ProvideServices<IRabbitMqMassTransitBusService>().StartBusAsync();
            startup.ProvideServices<IRabbitMqMassTransitBusService>().SendFirstMessage();
            Console.ReadLine();
        }
    }



}
