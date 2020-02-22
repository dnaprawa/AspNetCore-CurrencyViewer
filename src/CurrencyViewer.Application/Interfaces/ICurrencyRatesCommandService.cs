using CurrencyViewer.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Interfaces
{
    public interface ICurrencyRatesCommandService
    {
        Task SaveCurrencyRates(IEnumerable<CurrencyRateDto> dto);
    }
}
