using CurrencyViewer.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Interfaces
{
    public interface ICurrencyRatesQueryService
    {
        Task<CurrencyRateAverageViewModel> GetAverageCurrencyRates(DateTime dateFrom, DateTime dateTo);
        Task<IEnumerable<CurrencyRateViewModel>> GetCurrencyRatesBetweenDates(DateTime dateFrom, DateTime dateTo);
    }
}
