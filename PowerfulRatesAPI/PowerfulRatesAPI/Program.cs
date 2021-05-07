﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EventContracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerfulRatesAPI.Services;

namespace PowerfulRatesAPI
{
    class Program
    {
        // public static IConfiguration Configuration { get; set; }
        public static async Task Main(string[] args)
        {
            using Microsoft.Extensions.Hosting.IHost host = CreateHostBuilder(args).Build();

            var services = Startup.ConfigureServices(args);

            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<IRabbitMqMassTransitBus>().StartBusAsync();

        }
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args);


    }



}
