using CurrencyViewer.Application.Exceptions;
using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Services
{
    public class CurrencyRatesBetweenDatesReceiver : ICurrencyRatesBetweenDatesReceiver
    {
        private readonly CurrencyRatesConfig _config;
        private readonly IHttpClientFactory _clientFactory;
        public CurrencyRatesBetweenDatesReceiver(IOptions<CurrencyRatesConfig> options, IHttpClientFactory clientFactory)
        {
            _config = options.Value;
            _clientFactory = clientFactory;
        }

        public Task<IEnumerable<CurrencyRateDto>> GetCurrencyRatesBetweenDaysAsync(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom > dateTo)
            {
                throw new InvalidParameterException("DateFrom cannot be greater than DateTo");
            }

            if (dateFrom < DateTime.UtcNow.AddDays(-90))
            {
                throw new BadRequestException("Cannot find data for period longer than 90 days");
            }

            return null;
        }
    }
}
