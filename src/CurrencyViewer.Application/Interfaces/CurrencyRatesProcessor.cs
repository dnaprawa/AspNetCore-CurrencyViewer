using CurrencyViewer.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Interfaces
{
    public interface ICurrencyRatesProcessor
    {
        Task SaveToDatabase(IEnumerable<CurrencyRate> currencyRates);
    }
}
