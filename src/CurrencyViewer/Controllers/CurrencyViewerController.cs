using CurrencyViewer.API.Authentication;
using CurrencyViewer.Application.Infrastructure;
using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyViewer.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/currency")]
    [Authorize]
    public class CurrencyViewerController : ControllerBase
    {
        private readonly ICurrencyRatesQueryService _currencyRatesQueryService;
        private readonly ICurrencyRatesBetweenDatesReceiver _currencyRatesBetweenDatesReceiver;
        private readonly ICurrencyRatesCommandService _commandService;
        private readonly CurrencyRatesConfig _config;
        public CurrencyViewerController(ICurrencyRatesQueryService currencyRatesQueryService,
            ICurrencyRatesBetweenDatesReceiver currencyRatesBetweenDatesReceiver,
            ICurrencyRatesCommandService commandService,
            IOptions<CurrencyRatesConfig> config)
        {
            _currencyRatesQueryService = currencyRatesQueryService;
            _currencyRatesBetweenDatesReceiver = currencyRatesBetweenDatesReceiver;
            _commandService = commandService;
            _config = config.Value;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyRateViewModel>>> Get([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _currencyRatesQueryService.GetCurrencyRatesBetweenDates(dateFrom, dateTo);
            if (result.Count() >= ((dateTo - dateFrom).TotalDays * _config.CurrencyCodes.Count()))
            {
                return Ok(result);
            }

            var retrieved = await _currencyRatesBetweenDatesReceiver.GetCurrencyRatesBetweenDaysAsync(dateFrom, dateTo);
            await _commandService.SaveCurrencyRates(retrieved);

            var newResult = await _currencyRatesQueryService.GetCurrencyRatesBetweenDates(dateFrom, dateTo);

            return Ok(newResult);
        }

        [HttpGet("average")]
        [Authorize(Policy = Policies.ReadonlyUsers)]
        public async Task<ActionResult<IEnumerable<CurrencyRateAverageViewModel>>> GetAverageAsync([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dateFrom == null || dateTo == null)
            {
                return BadRequest("Invalid parameters");
            }

            var result = await _currencyRatesQueryService.GetAverageCurrencyRates(dateFrom, dateTo);

            return Ok(result);
        }
    }
}
