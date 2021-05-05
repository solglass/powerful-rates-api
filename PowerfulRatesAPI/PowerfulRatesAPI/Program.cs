using System;
using System.Threading.Tasks;
using EventContracts;
using MassTransit;

namespace PowerfulRatesAPI
{
    class Program
    {
        public static async Task Main()
        {      
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg => cfg.Host("localhost", hst =>
            {
                hst.Username("guest");
                hst.Password("guest");
            }));
            

            await busControl.StartAsync();
            try
            {
                while (true)
                {
                    string value = await Task.Run(() =>
                    {
                        Console.WriteLine("Enter message (or quit to exit)");
                        Console.Write("> ");
                        Console.ReadLine();
                        return CurrencyRates.GetCurrencyRates();
                    });

                    if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                        break;

                    await busControl.Publish<ValueEntered>(new
                    {
                        Value = value
                    });
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
