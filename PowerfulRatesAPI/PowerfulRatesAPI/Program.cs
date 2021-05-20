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
        private static Startup _startup;
        public static async Task Main(string[] args)
        {
            _startup = new Startup();
            await _startup.ProvideService<ICurrencyRatesSenderService>().SendFirstMessage();
            Console.ReadLine();
        }
    }

}
