using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerfulRatesWebAPI
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using EventContracts;
    using MassTransit;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

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
            var result = new CurrencyRates();
            await _publishEndpoint.Publish<ValueEntered>(new
            {
                Value = result.GetCurrencyRates()
            }); 

            return Ok();
        }

        /// <summary>
        /// Get json CurrencyRates
        /// </summary>
        /// <returns>Get json CurrencyRates</returns>
        // https://localhost:44365/api/transaction/42
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet("rates")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetRates()
        {
            //var result = new CurrencyRates().GetCurrencyRates();
            var result = new CurrencyRates();

            return Ok(result.GetCurrencyRates());
        }
    }
}

