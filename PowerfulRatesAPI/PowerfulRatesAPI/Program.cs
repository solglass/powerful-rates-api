using System;
using System.Threading;
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
                        return CurrencyRates.GetCurrencyRates();
                    });

                    await busControl.Publish<ValueEntered>(new
                    {
                        Value = value
                    });
                    Thread.Sleep(36000000);
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
