using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventContracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PowerfulRatesAPI.Services
{
    public class RabbitMqMassTransitBus : IRabbitMqMassTransitBus
    {
        private IBusControl _busControl;
        private string _host;
        private string _login;
        private string _password;
        private ICurrencyRates _currencyRates;

        public RabbitMqMassTransitBus(IConfiguration config, ICurrencyRates currencyRates)
        {
            _host = config.GetSection("Host").Value;
            _login = config.GetSection("Login").Value;
            _password = config.GetSection("Password").Value;
            _currencyRates = currencyRates;

            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg => cfg.Host(_host, hst =>
            {
                hst.Username(_login);
                hst.Password(_password);
            }));
        }
        public async Task StartBusAsync()
        {

            await _busControl.StartAsync();
            try
            {
                while (true)
                {
                    string value = await Task.Run(() =>
                    {
                        var currencyRates = _currencyRates.GetCurrencyRates();
                        Console.WriteLine($"The mesage was sent in {DateTime.Now} ");
                        return currencyRates;
                    });

                    await _busControl.Publish<ValueEntered>(new
                    {
                        Value = value
                    });
                    Thread.Sleep(3600000);
                }
            }
            finally
            {
                await _busControl.StopAsync();
            }
            
        }
    }
}
