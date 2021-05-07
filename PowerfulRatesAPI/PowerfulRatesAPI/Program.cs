using System;
using System.Threading.Tasks;
using EventContracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Timers;
using System.Collections.Generic;

namespace PowerfulRatesAPI
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }
        private static Timer _aTimer;
        public static async Task Main(string[] args)
        {
            using Microsoft.Extensions.Hosting.IHost host = CreateHostBuilder(args).Build();
            var builder = new ConfigurationBuilder()
               .AddEnvironmentVariables()
               .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
           
            _aTimer = new Timer();
            _aTimer.Interval = 5000;
            _aTimer.Elapsed += OnTimedEvent;
            _aTimer.AutoReset = true;           
            _aTimer.Enabled = true;
            await host.RunAsync();
        }
        private async static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg => cfg.Host(Configuration.GetSection("Host").Value, hst =>
            {
                hst.Username(Configuration.GetSection("Login").Value);
                hst.Password(Configuration.GetSection("Password").Value);
            }));
            await busControl.StartAsync();
            try
            {

                Dictionary<string, decimal> value = await Task.Run(() =>
                {
                    var currencyRates = CurrencyRatesSource.GetCurrencyRates();
                    Console.WriteLine($"I already sent this shit in {DateTime.Now} you jerk!");
                    return currencyRates;
                });

                await busControl.Publish<CurrencyRates>(new
                {
                    Value = value
                });

            }
            finally
            {
                await busControl.StopAsync();
            }
        }
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args);
    } 
}
