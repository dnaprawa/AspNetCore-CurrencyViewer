using CurrencyViewer.Application.Infrastructure;
using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
using CurrencyViewer.Domain;
using CurrencyViewer.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Services
{
    public class CurrencyRatesCommandService : ICurrencyRatesCommandService
    {
        private readonly CurrencyDbContext _currencyDbContext;
        public CurrencyRatesCommandService(CurrencyDbContext currencyDbContext)
        {
            _currencyDbContext = currencyDbContext;
        }
        public async Task SaveCurrencyRates(IEnumerable<CurrencyRateDto> dtos)
        {
            var entities = new List<CurrencyRate>();
            foreach (var dto in dtos)
            {
                var singleCurrency = CurrencyMapper.MapyMany(dto);
                entities.AddRange(singleCurrency);
            }
            
            var allEntities = _currencyDbContext.CurrencyRates.ToList();

            var toAdd = entities
                .Where(x => !allEntities.Any(e => e.Date == x.Date && e.CurrencyType == x.CurrencyType));

            _currencyDbContext.CurrencyRates.AddRange(toAdd);

            await _currencyDbContext.SaveChangesAsync();
        }
    }
}
