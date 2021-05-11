using System;
using System.Collections.Generic;
using System.Text;

namespace PowerfulRatesAPI.Settings
{
    public class AppSettings
    {
        public string Host { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string CurrencyRatesSource { get; set; }
        public int Interval { get; set; }
    }
}
