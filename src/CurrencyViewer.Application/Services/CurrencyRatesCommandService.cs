using CurrencyViewer.Application.Infrastructure;
using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
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
            var entities = dtos.Select(x => CurrencyMapper.MapFromDto(x));

            foreach (var entity in entities)
            {
                if(!_currencyDbContext.CurrencyRates.Any(x => x.Date == entity.Date && x.CurrencyType == x.CurrencyType))
                {
                    await _currencyDbContext.CurrencyRates.AddAsync(entity);
                }
                else
                {
                    var currentValue = _currencyDbContext.CurrencyRates
                        .Single(x => x.Date == entity.Date && x.CurrencyType == x.CurrencyType);

                    currentValue.Update(entity.BidValue, entity.AskValue);
                }
            }

            await _currencyDbContext.SaveChangesAsync();
        }
    }
}
