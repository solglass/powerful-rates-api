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
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg => cfg.Host("80.78.240.16", hst => 
            {
                hst.Username("volodya22");
                hst.Password("qwe!@#");
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
                        return Console.ReadLine();
                    });

                    if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                        break;

                    await busControl.Publish<TestMessage>(new
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
