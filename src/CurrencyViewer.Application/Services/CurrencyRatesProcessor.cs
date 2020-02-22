using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Application
{
    public class CurrencyRatesProcessor : ICurrencyRatesProcessor
    {
        public Task SaveToDatabaseAsync(IEnumerable<CurrencyRate> currencyRates)
        {
            throw new NotImplementedException();
        }
    }
}
