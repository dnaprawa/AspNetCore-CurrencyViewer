using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
using System;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Services
{
    public class CurrencyRatesQueryService : ICurrencyRatesQueryService
    {
        public Task<CurrencyRateAverageViewModel> GetAverageCurrencyRates(DateTime dateFrom, DateTime dateTo)
        {
            throw new NotImplementedException();
        }

        public Task<CurrencyRateViewModel> GetCurrencyRatesBetweenDates(DateTime dateFrom, DateTime dateTo)
        {
            throw new NotImplementedException();
        }
    }
}
