using CurrencyViewer.Application.Models;
using CurrencyViewer.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Application
{
    public class CurrencyRatesReceiver : ICurrencyRatesReceiver
    {
        private readonly CurrencyRatesConfig config;
        public CurrencyRatesReceiver(IOptions<CurrencyRatesConfig> options)
        {
            config = options.Value;
        }

        public Task<IEnumerable<CurrencyRate>> GetCurrencyRatesAsync(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
