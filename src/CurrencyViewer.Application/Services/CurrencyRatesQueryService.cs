using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Services
{
    public class CurrencyRatesQueryService : ICurrencyRatesQueryService
    {
        public Task<IEnumerable<CurrencyRateViewModel>> GetAverageCurrencyRates(DateTime dateFrom, DateTime dateTo)
        {
            throw new NotImplementedException();
        }
    }
}
