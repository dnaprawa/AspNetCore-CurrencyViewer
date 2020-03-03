using CurrencyViewer.Application.Exceptions;
using CurrencyViewer.Application.Infrastructure;
using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
using CurrencyViewer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Services
{
    public class CurrencyRatesQueryService : ICurrencyRatesQueryService
    {
        private readonly ICurrencyRatesBetweenDatesReceiver _currencyRatesBetweenDatesReceiver;
        private readonly CurrencyDbContext _currencyDbContext;
        public CurrencyRatesQueryService(ICurrencyRatesBetweenDatesReceiver currencyRatesBetweenDatesReceiver,
            CurrencyDbContext currencyDbContext)
        {
            _currencyRatesBetweenDatesReceiver = currencyRatesBetweenDatesReceiver;
            _currencyDbContext = currencyDbContext;
        }

        public Task<CurrencyRateAverageViewModel> GetAverageCurrencyRates(DateTime dateFrom, DateTime dateTo)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CurrencyRateViewModel>> GetCurrencyRatesBetweenDates(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom > dateTo)
            {
                throw new InvalidParameterException("DateFrom cannot be greater than DateTo");
            }

            if (dateFrom < DateTime.UtcNow.AddDays(-90))
            {
                throw new BadRequestException("Cannot find data in period longer than 90 days");
            };

            var inDb = await _currencyDbContext.CurrencyRates
                .Where(x => x.Date >= dateFrom && x.Date <= dateTo)
                .OrderBy(x => x.Date)
                .ToListAsync();

            return inDb.Select(x => CurrencyMapper.MapFromDto(x));
        }
    }
}
