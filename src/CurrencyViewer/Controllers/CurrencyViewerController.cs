using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/currency")]
    public class CurrencyViewerController : ControllerBase
    {
        private readonly ICurrencyRatesQueryService _currencyRatesQueryService;
        public CurrencyViewerController(ICurrencyRatesQueryService currencyRatesQueryService)
        {
            _currencyRatesQueryService = currencyRatesQueryService;
        }

        [HttpGet("average")]
        public async Task<ActionResult<IEnumerable<CurrencyRateViewModel>>> GetAverageAsync([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {   
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(dateFrom == null || dateTo == null)
            {
                return BadRequest("Invalid parameters");
            }

            var result = await _currencyRatesQueryService.GetAverageCurrencyRates(dateFrom, dateTo);

            return Ok(result);
        }


    }
}
