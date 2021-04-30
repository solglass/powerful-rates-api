using EventContracts;
using MassTransit;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerfulRatesWebAPI.Jobs
{
    public class RatesSender : IJob
    {
        public IPublishEndpoint _publishEndpoint;

        public Task Execute(IJobExecutionContext context)
        {

            return _publishEndpoint.Publish<ValueEntered>(new
            {
                Value = CurrencyRates.GetCurrencyRates()
            });
        }
    }
}
