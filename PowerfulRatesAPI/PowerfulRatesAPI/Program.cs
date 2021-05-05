using System;
using System.Threading;
using System.Threading.Tasks;
using EventContracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PowerfulRatesAPI
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static async Task Main(string[] args)
        {
            using Microsoft.Extensions.Hosting.IHost host = CreateHostBuilder(args).Build();
            var builder = new ConfigurationBuilder()
               .AddEnvironmentVariables()
               .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg => cfg.Host(Configuration.GetSection("Host").Value, hst =>
            {
                hst.Username(Configuration.GetSection("Login").Value);
                hst.Password(Configuration.GetSection("Password").Value);
            }));
            
            await busControl.StartAsync();
            try
            {
                while (true)
                {
                    string value = await Task.Run(() =>
                    {
                        var currencyRates = CurrencyRates.GetCurrencyRates();
                        Console.WriteLine($"I already sent this shit in {DateTime.Now} you jerk!");
                        return currencyRates;
                    });

                    await busControl.Publish<ValueEntered>(new
                    {
                        Value = value
                    });
                    Thread.Sleep(3600000);
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
            await host.RunAsync();
        }
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args);
    } 
}
