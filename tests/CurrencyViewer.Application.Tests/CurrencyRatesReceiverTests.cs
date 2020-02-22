using CurrencyViewer.Application.Models;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyViewer.Application.Tests
{
    public class CurrencyRatesReceiverTests
    {

        [Fact]
        public async Task ShouldReturnAnythingForCurrentDate()
        {
            var options = new Mock<IOptions<CurrencyRatesConfig>>();

            ICurrencyRatesReceiver receiver = new CurrencyRatesReceiver(options.Object);

            var result = await receiver.GetCurrencyRatesAsync(DateTime.UtcNow.Date);

            Assert.NotEmpty(result);
        }
    }
}
