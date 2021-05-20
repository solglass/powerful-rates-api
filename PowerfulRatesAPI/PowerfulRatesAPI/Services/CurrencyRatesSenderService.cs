using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PowerfulRatesAPI.Settings;
using EventContracts;
using PowerfulRatesAPI.Utils;
using System.Timers;
using PowerfulRatesAPI.CustomExceptions;

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
                await _publisherService.PublishAsync(new CurrencyRates { Value = rates });
                await Console.Out.WriteLineAsync($"Currency rates were sent in {DateTime.Now} ");
            }
            catch (ServiceUnavailableException exception)
            {
                await _publisherService.PublishAsync(new ErrorMessage { Value = exception.ErrorMessage });
                await Console.Out.WriteLineAsync($"Exception: {exception.ErrorMessage}, with statuscode: {exception.StatusCode} was sent in {DateTime.Now} ");
            }
            catch (ParsingException exception)
            {
                await _publisherService.PublishAsync(new ErrorMessage { Value = exception.ErrorMessage });
                await Console.Out.WriteLineAsync($"Exception: {exception.ErrorMessage}, with statuscode: {exception.StatusCode} was sent in {DateTime.Now} ");
            }
            catch (Exception exception)
            {
                await _publisherService.PublishAsync(new ErrorMessage { Value = exception.Message });
                await Console.Out.WriteLineAsync($"Exception: {exception.Message} was sent in {DateTime.Now} ");
            }
        }
    }
}

