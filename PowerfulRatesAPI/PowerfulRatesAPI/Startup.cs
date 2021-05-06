using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerfulRatesAPI.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PowerfulRatesAPI
{
    public class Startup
    {


        public static IServiceCollection ConfigureServices(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            var config = SetupConfiguration(args);
            services.AddSingleton(config);
            services.RegistrateServicesConfig();

            return services;
        }

        private static IConfiguration SetupConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }


    }


}
