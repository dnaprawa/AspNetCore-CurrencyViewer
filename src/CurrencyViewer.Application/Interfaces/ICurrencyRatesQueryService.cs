using CurrencyViewer.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Interfaces
{
    public interface ICurrencyRatesQueryService
    {
        Task<IEnumerable<CurrencyRateViewModel>> GetAverageCurrencyRates(DateTime dateFrom, DateTime dateTo);
    }
}
