using CurrencyViewer.Application.Models;
using CurrencyViewer.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Application
{
    public interface ICurrencyRatesReceiver
    {
        Task<IEnumerable<CurrencyRateDto>> GetCurrencyRatesAsync(DateTime date);
    }
}
