using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Options;
using System.Timers;
using Timer = System.Timers.Timer;
using PowerfulRatesAPI.Settings;
using EventContracts;

namespace PowerfulRatesAPI.Services
{
    public class RabbitMqMassTransitBusService : IRabbitMqMassTransitBusService
    {
        private IBusControl _busControl;
        private string _host;
        private string _login;
        private string _password;
        private ICurrencyRatesService _currencyRates;
        private Timer _aTimer;
        private readonly int _interval;

        public RabbitMqMassTransitBusService(IOptions<AppSettings> options, ICurrencyRatesService currencyRates)
        {
            _host = options.Value.RATES_API_RABBITMQ_HOST;
            _login = options.Value.RATES_API_RABBITMQ_LOGIN;
            _password = options.Value.RATES_API_RABBITMQ_PASSWORD;
            _interval = options.Value.RATES_API_TIMER_INTERVAL;
            _currencyRates = currencyRates;

            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg => cfg.Host(_host, hst =>
            {
                hst.Username(_login);
                hst.Password(_password);
            }));

        }

        public void SetupTimer()
        {
            _aTimer = new Timer
            {
                Interval = _interval,
                AutoReset = true,
                Enabled = true
            };
            _aTimer.Elapsed += SendMessagesAsync;
            
        }

        public void SendFirstMessage() => SendMessagesAsync(null, null);

        public async Task StartBusAsync() =>
            await _busControl.StartAsync();

        private async void SendMessagesAsync(Object source, ElapsedEventArgs e)
        {

            try
            {
                Dictionary<string, decimal> value = await Task.Run(() =>
                {
                    var currencyRates = _currencyRates.GetCurrencyRates();
                    Console.WriteLine($"Currency rates were obtained in {DateTime.Now} ");
                    return currencyRates;
                });

                await _busControl.Publish<CurrencyRates>(new
                {
                    Value = value
                });
            }


            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
        }
    }
}

