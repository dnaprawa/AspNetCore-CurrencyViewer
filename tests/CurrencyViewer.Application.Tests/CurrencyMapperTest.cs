using CurrencyViewer.Application.Infrastructure;
using CurrencyViewer.Application.Models;
using System.Collections.Generic;
using Xunit;

namespace CurrencyViewer.Application.Tests
{
    public class CurrencyMapperTest
    {
        [Fact]
        public void ShouldMapFromDtoToEntity()
        {
            var dto = new CurrencyRateDto()
            {
                Code = "usd",
                Currency = "dolar",
                Rates = new List<Rate>()
                {
                    new Rate()
                    {
                        Ask = 3.7800,
                        Bid = 3.8853,
                        EffectiveDate = "2020-02-01"
                    }
                }
            };

            var entity = CurrencyMapper.MapSingle(dto);

            Assert.NotNull(entity);
        }
    }
}
