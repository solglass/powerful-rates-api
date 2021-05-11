using System;
using System.Timers;

namespace PowerfulRatesAPI.Utils
{
    public class CurrencyRatesTimer
    {
        private Timer _timer;
        public CurrencyRatesTimer(int interval)
        {
            _timer = new Timer
            {
                Interval = interval,
                AutoReset = true,
                Enabled = true
            };
        }

        public void SubscribeToTimer(ElapsedEventHandler method)
        {
            _timer.Elapsed += method;
        }
        public void UnsubscribeFromTimer(ElapsedEventHandler method)
        {
            _timer.Elapsed -= method;
        }
    }
}
