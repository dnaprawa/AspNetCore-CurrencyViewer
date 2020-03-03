using CurrencyViewer.Application.Models;
using Microsoft.Extensions.Options;
using Moq;

namespace CurrencyViewer.Application.Tests.Infrastructure
{
    public static class ConfigFactory
    {
        public static IOptions<CurrencyRatesConfig> GetConfig(CurrencyRatesConfig expectedResult = null)
        {
            var options = new Mock<IOptions<CurrencyRatesConfig>>();

            var returnValue = expectedResult ?? new CurrencyRatesConfig() { BaseUrl = "http://api.nbp.pl", CurrencyCodes = new string[] { "usd", "eur" } };
            
            options.SetupGet(x => x.Value).Returns(returnValue);

            return options.Object;
        }
    }
}
