using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Services
{
    public class CurrencyRatesCommandService : ICurrencyRatesCommandService
    {
        public Task SaveCurrencyRates(IEnumerable<CurrencyRateDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
