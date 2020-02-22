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
            ICurrencyRatesReceiver receiver = new CurrencyRatesReceiver();

            var result = await receiver.GetCurrencyRatesAsync(DateTime.UtcNow.Date);

            Assert.NotEmpty(result);
        }
    }
}
