using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerfulRatesWebAPI
{
    using System.Threading.Tasks;
    using EventContracts;
    using MassTransit;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using PowerfulRatesWebAPI.Jobs;

    [ApiController]
    [Route("api/[controller]")]

    public class ValueController : ControllerBase
    {
        readonly IPublishEndpoint _publishEndpoint;

        public ValueController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("value")]
        public async Task<ActionResult> Post()
        {
            await _publishEndpoint.Publish<ValueEntered>(new
            {
                Value = CurrencyRates.GetCurrencyRates()
            }); 

            return Ok();
        }

        /// <summary> 
        /// Start sending CurrencyRates 
        /// </summary> 
        /// <returns>Start sending CurrencyRates </returns> 
        // https://localhost:44365/api/rates
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("rates")]
        public ActionResult GetRates()
        {
            Scheduler.Start();
            return Ok();
        }
    }
}

