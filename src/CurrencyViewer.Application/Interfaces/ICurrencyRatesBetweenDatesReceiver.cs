using CurrencyViewer.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Interfaces
{
    public interface ICurrencyRatesBetweenDatesReceiver
    {
        Task<IEnumerable<CurrencyRateDto>> GetCurrencyRatesBetweenDaysAsync(DateTime dateFrom, DateTime dateTo);
    }
}
