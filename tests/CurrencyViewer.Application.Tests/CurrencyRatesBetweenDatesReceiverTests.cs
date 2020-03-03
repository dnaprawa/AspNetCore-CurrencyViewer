using CurrencyViewer.Application.Exceptions;
using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
using CurrencyViewer.Application.Services;
using CurrencyViewer.Application.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyViewer.Application.Tests
{
    public class CurrencyRatesBetweenDatesReceiverTests
    {

        [Fact]
        public async Task When_DateFrom_Is_Greater_Than_DateFrom_Should_Throw_Exception()
        {
            var config = ConfigFactory.GetConfig();
            var httpClientFactory = HttpClientFactoryProvider.GetHttpClientFactory(new CurrencyRateDto() { Rates = new List<Rate>() });

            ICurrencyRatesBetweenDatesReceiver receiver = new CurrencyRatesBetweenDatesReceiver(config, httpClientFactory);

            Task result() => receiver.GetCurrencyRatesBetweenDaysAsync(DateTime.UtcNow.Date.AddDays(-2), DateTime.UtcNow.Date.AddDays(-1));

            await Assert.ThrowsAsync<InvalidParameterException>(result);
        }
    }
}
