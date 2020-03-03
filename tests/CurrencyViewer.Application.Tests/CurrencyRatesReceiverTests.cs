using CurrencyViewer.Application.Models;
using CurrencyViewer.Application.Tests.Infrastructure;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyViewer.Application.Tests
{
    public class CurrencyRatesReceiverTests
    {

        [Fact]
        public async Task Should_Return_Anything_For_Valid_Request()
        {
            var config = ConfigFactory.GetConfig();
            var httpClientFactory = HttpClientFactoryProvider.GetHttpClientFactory(new CurrencyRateDto() { Rates = new List<Rate>() });

            ICurrencyRatesReceiver receiver = new CurrencyRatesReceiver(config, httpClientFactory);

            var result = await receiver.GetCurrencyRatesAsync(DateTime.UtcNow.Date);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task When_Config_Is_Invalid_Should_Throw_Exception()
        {
            var options = new Mock<IOptions<CurrencyRatesConfig>>();

            var httpClientFactory = HttpClientFactoryProvider.GetHttpClientFactory(new CurrencyRateDto() { Rates = new List<Rate>() });

            ICurrencyRatesReceiver receiver = new CurrencyRatesReceiver(options.Object, httpClientFactory);

            Task result() => receiver.GetCurrencyRatesAsync(DateTime.UtcNow.Date);

            await Assert.ThrowsAsync<InvalidOperationException>(result);
        }
    }
}
