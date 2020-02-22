using CurrencyViewer.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Application
{
    public class CurrencyRatesReceiver : ICurrencyRatesReceiver
    {
        public Task<IEnumerable<CurrencyRate>> GetCurrencyRatesAsync(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
