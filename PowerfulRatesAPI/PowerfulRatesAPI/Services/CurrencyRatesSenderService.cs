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
    public class CurrencyRatesSenderService : ICurrencyRatesSenderService
    {
        private IPublisherService _publisherService;
        private ICurrencyRatesGetterService _currencyRates;
        private CurrencyRatesTimer _timer;

        public CurrencyRatesSenderService(IOptions<AppSettings> options, ICurrencyRatesGetterService currencyRates, IPublisherService publisherService)
        {
            _currencyRates = currencyRates;
            _publisherService = publisherService;
            _timer = new CurrencyRatesTimer(options.Value.RATES_API_TIMER_INTERVAL);
            _timer.SubscribeToTimer((Object source, ElapsedEventArgs e) => SendMessagesAsync(source, e));
        }
 
        public async Task SendFirstMessage() => await SendMessagesAsync(null, null);

        private async Task SendMessagesAsync(Object source, ElapsedEventArgs e)
        {
            try
            {
                var rates = await _currencyRates.GetCurrencyRatesAsync();
                if (rates != null && rates.Count > 0)
                {
                    await _publisherService.PublishAsync(new CurrencyRates { Value = rates });
                    await Console.Out.WriteLineAsync($"Currency rates were sent in {DateTime.Now} ");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

