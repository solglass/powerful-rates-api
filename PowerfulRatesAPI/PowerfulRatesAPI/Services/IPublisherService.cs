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
    public interface IPublisherService
    {
        Task PublishAsync(object message);
    }
}

