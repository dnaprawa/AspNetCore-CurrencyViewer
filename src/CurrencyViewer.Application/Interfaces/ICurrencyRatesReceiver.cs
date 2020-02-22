using CurrencyViewer.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Application
{
    public interface ICurrencyRatesReceiver
    {
        Task<IEnumerable<CurrencyRate>> GetCurrencyRatesAsync(DateTime date);
    }
}
