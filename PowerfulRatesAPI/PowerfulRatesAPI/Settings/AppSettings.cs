using System;
using System.Collections.Generic;
using System.Text;

namespace PowerfulRatesAPI.Settings
{
    public class AppSettings
    {
        public string RATES_API_RABBITMQ_HOST { get; set; }
        public string RATES_API_RABBITMQ_LOGIN { get; set; }
        public string RATES_API_RABBITMQ_PASSWORD { get; set; }
        public string CURRENCY_RATES_SOURCE { get; set; }
        public int RATES_API_TIMER_INTERVAL { get; set; }
    }
}
