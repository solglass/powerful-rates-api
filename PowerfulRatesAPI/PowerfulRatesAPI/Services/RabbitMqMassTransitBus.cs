﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventContracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Timers;
using System.Threading;
using Timer = System.Timers.Timer;

namespace PowerfulRatesAPI.Services
{
    public class RabbitMqMassTransitBus : IRabbitMqMassTransitBus
    {
        private IBusControl _busControl;
        private string _host;
        private string _login;
        private string _password;
        private ICurrencyRates _currencyRates;
        private Timer _aTimer;
        private const int _interval = 5000;

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
        //async
        public async Task SetupTimedEvent()
        {
            _aTimer = new Timer
            {
                Interval = _interval,
                AutoReset = true,
                Enabled = true
            };
           // _aTimer.Elapsed += StartBusAsync;
        }
        //public async void StartBusAsync(Object source, ElapsedEventArgs e)
        public async Task StartBusAsync()
        {

            await _busControl.StartAsync();
            while (true)
            {
                try  
                {
                    Dictionary<string, decimal> value = await Task.Run(() =>
                    {
                        var currencyRates = _currencyRates.GetCurrencyRates();
                        Console.WriteLine($"The mesage was sent in {DateTime.Now} ");
                        return currencyRates;
                    });

                    await _busControl.Publish<CurrencyRates>(new
                    {
                        Value = value
                    });
                    Thread.Sleep(_interval);
                }


                finally
                {
                    await _busControl.StopAsync();
                }
               
            }

        }
    }
}
