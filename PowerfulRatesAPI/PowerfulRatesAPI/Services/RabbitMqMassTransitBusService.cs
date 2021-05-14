using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Options;
using PowerfulRatesAPI.Settings;
using EventContracts;
using PowerfulRatesAPI.Utils;
using System.Timers;

namespace PowerfulRatesAPI.Services
{
    public class RabbitMqMassTransitBusService : IRabbitMqMassTransitBusService
    {
        private IBusControl _busControl;
        private ICurrencyRatesService _currencyRates;
        private CurrencyRatesTimer _timer;

        public RabbitMqMassTransitBusService(IOptions<AppSettings> options, ICurrencyRatesService currencyRates)
        {
            _currencyRates = currencyRates;
            _timer = new CurrencyRatesTimer(options.Value.RATES_API_TIMER_INTERVAL);
            _timer.SubscribeToTimer((Object source, ElapsedEventArgs e) => SendMessagesAsync(source, e));

            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg => cfg.Host(options.Value.RATES_API_RABBITMQ_HOST, hst =>
            {
                hst.Username(options.Value.RATES_API_RABBITMQ_LOGIN);
                hst.Password(options.Value.RATES_API_RABBITMQ_PASSWORD);
            }));
        }

        
        public async Task SendFirstMessage() => await SendMessagesAsync(null, null);

        public async Task StartBusAsync() =>
            await _busControl.StartAsync();

        private async Task SendMessagesAsync(Object source, ElapsedEventArgs e)
        {

            try
            {
                await _busControl.Publish<CurrencyRates>(new
                {
                    Value = _currencyRates.GetCurrencyRates()
                });
                await Console.Out.WriteLineAsync($"Currency rates were sent in {DateTime.Now} ");
            }


            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error occured: {ex.Message}");
            }
        }
    }
}

